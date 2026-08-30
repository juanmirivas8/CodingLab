import { Component, signal } from '@angular/core';

@Component({
  selector: 'text-playground-component',
  styleUrl: './text-playground-component.css',
  templateUrl: './text-playground-component.html',
})
export class TextPlaygroundComponent {
  protected readonly textValue = signal('');
}
