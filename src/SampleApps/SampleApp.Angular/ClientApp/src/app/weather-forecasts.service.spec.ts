import { TestBed } from '@angular/core/testing';

import { WeatherForecastsService } from './weather-forecasts.service';

describe('WeatherForecastsService', () => {
  let service: WeatherForecastsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WeatherForecastsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
