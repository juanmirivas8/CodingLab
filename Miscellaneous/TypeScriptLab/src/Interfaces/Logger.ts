export interface Logger {
    log(message: string): void;
    error(message: string): void;
    success(message: string): void;
}
