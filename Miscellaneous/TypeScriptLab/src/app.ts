// ==================== TIPOS Y INTERFACES ====================

interface Calculator {
    add(a: number, b: number): number;
    multiply(a: number, b: number): number;
}

interface Logger {
    log(message: string): void;
    error(message: string): void;
    success(message: string): void;
}

// ==================== IMPLEMENTACIONES ====================

class SimpleCalculator implements Calculator {
    add(a: number, b: number): number {
        return a + b;
    }
    
    multiply(a: number, b: number): number {
        return a * b;
    }
}

class ConsoleLogger implements Logger {
    private consolDiv: HTMLElement | null;
    
    constructor() {
        this.consolDiv = document.getElementById('console');
    }
    
    log(message: string): void {
        this.addEntry(message, 'info');
        console.log(message);
    }
    
    error(message: string): void {
        this.addEntry(message, 'error');
        console.error(message);
    }
    
    success(message: string): void {
        this.addEntry(message, 'success');
        console.log(message);
    }
    
    private addEntry(message: string, type: string): void {
        if (this.consolDiv) {
            const entry = document.createElement('div');
            entry.className = `log-entry ${type}`;
            entry.textContent = `[${new Date().toLocaleTimeString()}] ${message}`;
            this.consolDiv.appendChild(entry);
            this.consolDiv.scrollTop = this.consolDiv.scrollHeight;
        }
    }
}

class TextTransformer {
    toUpperCase(text: string): string {
        return text.toUpperCase();
    }
    
    toLowerCase(text: string): string {
        return text.toLowerCase();
    }
    
    capitalize(text: string): string {
        return text.charAt(0).toUpperCase() + text.slice(1).toLowerCase();
    }
}

class EmailValidator {
    validate(email: string): boolean {
        const emailRegex: RegExp = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return emailRegex.test(email);
    }
}

type Player = 'X' | 'O' | '';
type GameState = Player[];

class TicTacToe {
    private board: GameState;
    private currentPlayer: Player;
    private gameOver: boolean;
    private winner: Player;
    
    constructor() {
        this.board = Array(9).fill('');
        this.currentPlayer = 'X';
        this.gameOver = false;
        this.winner = '';
    }
    
    playMove(position: number): boolean {
        if (position < 0 || position > 8 || this.board[position] !== '' || this.gameOver) {
            return false;
        }
        
        this.board[position] = this.currentPlayer;
        
        if (this.checkWinner()) {
            this.winner = this.currentPlayer;
            this.gameOver = true;
            return true;
        }
        
        if (this.isBoardFull()) {
            this.gameOver = true;
            return true;
        }
        
        this.currentPlayer = this.currentPlayer === 'X' ? 'O' : 'X';
        return true;
    }
    
    private checkWinner(): boolean {
        const winningCombinations: number[][] = [
            [0, 1, 2],
            [3, 4, 5],
            [6, 7, 8],
            [0, 3, 6],
            [1, 4, 7],
            [2, 5, 8],
            [0, 4, 8],
            [2, 4, 6]
        ];
        
        return winningCombinations.some(combo =>
            this.board[combo[0]] === this.currentPlayer &&
            this.board[combo[1]] === this.currentPlayer &&
            this.board[combo[2]] === this.currentPlayer
        );
    }
    
    private isBoardFull(): boolean {
        return this.board.every(cell => cell !== '');
    }
    
    getBoard(): GameState {
        return [...this.board];
    }
    
    getCurrentPlayer(): Player {
        return this.currentPlayer;
    }
    
    isGameOver(): boolean {
        return this.gameOver;
    }
    
    getWinner(): Player {
        return this.winner;
    }
    
    reset(): void {
        this.board = Array(9).fill('');
        this.currentPlayer = 'X';
        this.gameOver = false;
        this.winner = '';
    }
}

// ==================== INSTANCIAS GLOBALES ====================

const calculator = new SimpleCalculator();
const logger = new ConsoleLogger();
const textTransformer = new TextTransformer();
const emailValidator = new EmailValidator();
const ticTacToe = new TicTacToe();

let counter: number = 0;

// ==================== FUNCIONES GLOBALES ====================

function calculateSum(): void {
    const num1Element = document.getElementById('num1') as HTMLInputElement;
    const num2Element = document.getElementById('num2') as HTMLInputElement;
    const outputDiv = document.getElementById('calcOutput');
    
    const num1: number = parseFloat(num1Element.value);
    const num2: number = parseFloat(num2Element.value);
    
    if (isNaN(num1) || isNaN(num2)) {
        logger.error('Por favor ingresa números válidos');
        return;
    }
    
    const result: number = calculator.add(num1, num2);
    if (outputDiv) {
        outputDiv.innerHTML = `<div class="log-entry success">${num1} + ${num2} = ${result}</div>`;
    }
    logger.success(`Suma calculada: ${num1} + ${num2} = ${result}`);
}

function calculateMultiply(): void {
    const num1Element = document.getElementById('num1') as HTMLInputElement;
    const num2Element = document.getElementById('num2') as HTMLInputElement;
    const outputDiv = document.getElementById('calcOutput');
    
    const num1: number = parseFloat(num1Element.value);
    const num2: number = parseFloat(num2Element.value);
    
    if (isNaN(num1) || isNaN(num2)) {
        logger.error('Por favor ingresa números válidos');
        return;
    }
    
    const result: number = calculator.multiply(num1, num2);
    if (outputDiv) {
        outputDiv.innerHTML = `<div class="log-entry success">${num1} × ${num2} = ${result}</div>`;
    }
    logger.success(`Multiplicación calculada: ${num1} × ${num2} = ${result}`);
}

function transformText(): void {
    const textInput = document.getElementById('textInput') as HTMLInputElement;
    const outputDiv = document.getElementById('textOutput');
    
    const text: string = textInput.value;
    
    if (text.trim() === '') {
        logger.error('Por favor ingresa un texto');
        return;
    }
    
    const transformed: string = textTransformer.toUpperCase(text);
    if (outputDiv) {
        outputDiv.innerHTML = `<div class="log-entry success">${transformed}</div>`;
    }
    logger.success(`Texto transformado: "${text}" → "${transformed}"`);
}

function validateEmail(): void {
    const emailInput = document.getElementById('emailInput') as HTMLInputElement;
    const outputDiv = document.getElementById('emailOutput');
    
    const email: string = emailInput.value;
    
    if (email.trim() === '') {
        logger.error('Por favor ingresa un email');
        return;
    }
    
    const isValid: boolean = emailValidator.validate(email);
    const message: string = isValid ? '✓ Email válido' : '✗ Email inválido';
    const className: string = isValid ? 'success' : 'error';
    
    if (outputDiv) {
        outputDiv.innerHTML = `<div class="log-entry ${className}">${message}</div>`;
    }
    logger.log(`Validación de email "${email}": ${message}`);
}

function increment(): void {
    counter++;
    updateCounter();
    logger.log(`Contador incrementado: ${counter}`);
}

function decrement(): void {
    counter--;
    updateCounter();
    logger.log(`Contador decrementado: ${counter}`);
}

function resetCounter(): void {
    counter = 0;
    updateCounter();
    logger.log('Contador reseteado a 0');
}

function updateCounter(): void {
    const counterDisplay = document.getElementById('counterDisplay');
    const outputDiv = document.getElementById('counterOutput');
    
    if (counterDisplay) {
        counterDisplay.textContent = counter.toString();
    }
    
    if (outputDiv) {
        outputDiv.innerHTML = `<div class="log-entry info">Valor actual: ${counter}</div>`;
    }
}

function playMove(position: number): void {
    if (ticTacToe.playMove(position)) {
        updateGameBoard();
        
        if (ticTacToe.isGameOver()) {
            const winner: Player = ticTacToe.getWinner();
            if (winner) {
                logger.success(`¡El jugador ${winner} ha ganado!`);
            } else {
                logger.log('¡Empate! El tablero está lleno');
            }
        } else {
            logger.log(`Turno del jugador: ${ticTacToe.getCurrentPlayer()}`);
        }
    }
}

function updateGameBoard(): void {
    const cells = document.querySelectorAll('.cell');
    const board = ticTacToe.getBoard();
    const currentPlayer = document.getElementById('currentPlayer');
    const gameOutput = document.getElementById('gameOutput');
    
    cells.forEach((cell, index) => {
        const button = cell as HTMLButtonElement;
        const value = board[index];
        button.textContent = value;
        button.disabled = value !== '' || ticTacToe.isGameOver();
        button.classList.remove('x', 'o');
        if (value === 'X') {
            button.classList.add('x');
        } else if (value === 'O') {
            button.classList.add('o');
        }
    });
    
    if (currentPlayer) {
        currentPlayer.textContent = ticTacToe.getCurrentPlayer();
    }
    
    if (ticTacToe.isGameOver()) {
        const winner = ticTacToe.getWinner();
        if (gameOutput) {
            const message = winner 
                ? `<div class="log-entry success">¡Ganador: ${winner}!</div>`
                : '<div class="log-entry info">¡Empate!</div>';
            gameOutput.innerHTML = message;
        }
    }
}

function resetGame(): void {
    ticTacToe.reset();
    updateGameBoard();
    const gameOutput = document.getElementById('gameOutput');
    if (gameOutput) {
        gameOutput.innerHTML = '';
    }
    logger.log('Nuevo juego iniciado');
}

// ==================== INICIALIZACIÓN ====================

document.addEventListener('DOMContentLoaded', (): void => {
    logger.success('TypeScript Lab cargado correctamente ✓');
    logger.log('Todos los módulos están listos para usar');
});

