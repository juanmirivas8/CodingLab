import { Logger } from '../Interfaces/Logger';

export class ConsoleLogger implements Logger {
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

