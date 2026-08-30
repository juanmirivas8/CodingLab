import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TextPlaygroundComponent } from './components/text-playground-component/text-playground-component';
import { SimpleCardComponent } from './components/simple-card-component/simple-card-component';

@Component({
  imports: [RouterOutlet, TextPlaygroundComponent, SimpleCardComponent],
  selector: 'app-root',
  styleUrl: './app.css',
  templateUrl: './app.html',
})
export class App {
  protected readonly title = signal('AngularLab');
}
