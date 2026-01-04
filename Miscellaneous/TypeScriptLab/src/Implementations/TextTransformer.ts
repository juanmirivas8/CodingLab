export class TextTransformer {
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

