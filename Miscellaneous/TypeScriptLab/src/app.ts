// ==================== IMPORTS ====================
import { ConsoleLogger } from './Implementations/ConsoleLogger';
import { TextTransformer } from './Implementations/TextTransformer';

// ==================== INSTANCIAS GLOBALES ====================
const logger = new ConsoleLogger();
const textTransformer = new TextTransformer();
const selectableFunctions = [textPlayground];

// ==================== FUNCIONES GLOBALES ====================

function textPlayground(): void {
    const textInput = document.getElementById('textPlaygroundInput') as HTMLInputElement;
    const outputDiv = document.getElementById('textPlayGroundOutput');

    const text: string = textInput.value;

    if (text.trim() === '') {
        logger.error('Por favor ingresa un texto');
        return;
    }
    
    let transformation : (text: string) => string;
    switch (text.length % 2) {
        case 0:
            logger.log('El texto tiene una longitud par');
            transformation = textTransformer.toLowerCase;
            break;
        default:
            logger.log('El texto tiene una longitud impar');
            transformation = textTransformer.toUpperCase;
            break;
    }
    
    const transformed: string = executeTextTransformation(text, transformation);
    if (outputDiv) {
        outputDiv.innerHTML = `<div class="log-entry success">${transformed}</div>`;
    }
    logger.success(`Texto transformado: "${text}" → "${transformed}"`);
}

function executeTextTransformation(text : string,transformer: (text: string) => string) : string {
    logger.log(`String to transform: ${arguments[0]}`); // a function can access arguments
    logger.log(`Using transformer: ${arguments[1]}`);
    return transformer(text);
}

// ==================== INICIALIZACIÓN ====================

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
        logger.log(`Ejecutando función: ${selectedFunctionName}`);
        selectedFunction();
    } else {
        logger.error(`Función no encontrada: ${selectedFunctionName}`);
    }
}

document.addEventListener('DOMContentLoaded', (): void => {
    populateTextPlaygroundDropdown();
    
    // Add click event listener to the "Go !" button
    const textPlaygroundButton = document.getElementById('textPlaygroundButton') as HTMLButtonElement;
    if (textPlaygroundButton) {
        textPlaygroundButton.addEventListener('click', executeSelectedFunction);
    }
    
    logger.success('TypeScript Lab cargado correctamente ✓');
    logger.log('Todos los módulos están listos para usar');
});

