import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WeatherForecastsComponent } from './weather-forecasts/weather-forecasts.component';

const routes: Routes = [
  { path: 'weather-forecasts', component: WeatherForecastsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
