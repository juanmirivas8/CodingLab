// ==================== IMPORTS ====================
import { ConsoleLogger } from './Implementations/ConsoleLogger';
import { TextTransformer } from './Implementations/TextTransformer';

// ==================== GLOBAL INSTANCES ====================
const logger = new ConsoleLogger();
const textTransformer = new TextTransformer();
const selectableFunctions = [textPlayground, objectPlayground];

// ==================== GLOBAL FUNCTIONS ====================
function objectPlayground(): void {
    type Guid = `${string}-${string}-${string}-${string}-${string}`; // Template literal type for GUID
    type Power = 'flight' | 'strength' | 'invisibility' | 'speed' | 'intelligence' | 3; // Union types
    type HeroBase = {
        id?: Guid;
        name: string;
        age: number;
    }

    type HeroProperties = {
        isActive?: boolean;
        power?: Power;
    }
    type Hero = HeroBase & HeroProperties; // Intersection types

    let thor: Hero = {
        name: 'Thor',
        age: 1500
    }

    function createHero(hero: Hero): Hero {
        const { name, ...rest } = hero;
        logger.success(`Hero created: ${name}, Age: ${rest.age}, Active: true`);
        
        return { name, age: rest.age, isActive: true };
    }
    createHero(thor);
}

function textPlayground(): void {
    const textInput = document.getElementById('textPlayPlaygroundInput') as HTMLInputElement;
    const outputDiv = document.getElementById('textPlayGroundOutput');
    const text: string = textInput.value; 
    

    if (text.trim() === '') {
        logger.error('Please enter some text');
        return;
    }

    let transformation: (text: string) => string;
    switch (text.length % 2) {
        case 0:
            logger.log('The text has an even length');
            transformation = textTransformer.toLowerCase;
            break;
        case 1:
            logger.log('The text has an odd length');
            transformation = textTransformer.toUpperCase;
            break;
        default:
            logger.log('The text has an odd length');
            transformation = textTransformer.toUpperCase;
            break;
    }

    const transformed: string = executeTextTransformation(text, transformation);
    if (outputDiv) {
        outputDiv.innerHTML = `<div class="log-entry success">${transformed}</div>`;
    }
    logger.success(`Transformed text: "${text}" → "${transformed}"`);
}

function executeTextTransformation(text: string, transformer: (text: string) => string): string {
    logger.log(`String to transform: ${arguments[0]}`); // a function can access arguments
    logger.log(`Using transformer: ${arguments[1]}`);
    return transformer(text);
}

// ==================== INITIALIZATION ====================

function populateTextPlaygroundDropdown(): void {
    const dropdown = document.getElementById('textPlaygroundDropdown') as HTMLSelectElement;
    if (dropdown) {
        dropdown.innerHTML = ''; // Clear existing options

        selectableFunctions.forEach((option) => {
            const optionElement = document.createElement('option');
            optionElement.value = option.name;
            optionElement.textContent = option.name;
            dropdown.appendChild(optionElement);
        });
        
        logger.log(`Dropdown populated with ${selectableFunctions.length} options`);
    }
}

function executeSelectedFunction(): void {
    const dropdown = document.getElementById('textPlaygroundDropdown') as HTMLSelectElement;
    const selectedFunctionName = dropdown.value;

    // Find the function by name
    const selectedFunction = selectableFunctions.find(fn => fn.name === selectedFunctionName);

    if (selectedFunction) {
        logger.log(`Executing function: ${selectedFunctionName}`);
        selectedFunction();
    } else {
        logger.error(`Function not found: ${selectedFunctionName}`);
    }
}

document.addEventListener('DOMContentLoaded', (): void => {
    populateTextPlaygroundDropdown();

    // Add click event listener to the "Go !" button
    const textPlaygroundButton = document.getElementById('textPlaygroundButton') as HTMLButtonElement;
    if (textPlaygroundButton) {
        textPlaygroundButton.addEventListener('click', executeSelectedFunction);
    }

    logger.success('TypeScript Lab loaded successfully ✓');
    logger.log('All modules are ready to use');
});
