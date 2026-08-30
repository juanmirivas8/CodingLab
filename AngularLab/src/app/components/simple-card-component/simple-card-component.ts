import { Component, input } from '@angular/core';

@Component({
  imports: [],
  selector: 'simple-card-component',
  styleUrl: './simple-card-component.css',
  templateUrl: './simple-card-component.html',
})
export class SimpleCardComponent {
  readonly title = input<string>('Default Card Title');
  readonly subtitle = input<string>('Default subtitle text');
}
