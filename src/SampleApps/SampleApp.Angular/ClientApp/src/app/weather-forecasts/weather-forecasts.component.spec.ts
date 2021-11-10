import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WeatherForecastsComponent } from './weather-forecasts.component';

describe('WeatherForecastsComponent', () => {
  let component: WeatherForecastsComponent;
  let fixture: ComponentFixture<WeatherForecastsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WeatherForecastsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WeatherForecastsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
