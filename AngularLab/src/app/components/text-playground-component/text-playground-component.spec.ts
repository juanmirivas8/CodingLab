import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TextPlaygroundComponent } from './text-playground-component';

describe('TextPlaygroundComponent', () => {
  let component: TextPlaygroundComponent;
  let fixture: ComponentFixture<TextPlaygroundComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TextPlaygroundComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(TextPlaygroundComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
