import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', loadComponent: () => import('./components/product-list/product-list.component').then(m => m.ProductListComponent) },
  { path: 'product/new', loadComponent: () => import('./components/product-form/product-form.component').then(m => m.ProductFormComponent) },
  { path: 'product/:id', loadComponent: () => import('./components/product-form/product-form.component').then(m => m.ProductFormComponent) },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
