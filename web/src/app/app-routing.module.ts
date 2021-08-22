import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CarrosListComponent } from './carros-list/carros-list.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'carros' },
  { path: 'carros', component: CarrosListComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
