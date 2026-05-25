import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Inicial } from './componentes/inicial/inicial';
import { Editar } from './componentes/editar/editar';
import { Contatos } from './componentes/contatos/contatos';
import { Cadastrar } from './componentes/cadastrar/cadastrar';

export const routes: Routes = [
  {path: 'inicial', component: Inicial},
  {path: 'contatos', component: Contatos},
  {path: 'editar/:id', component: Editar},
  {path: 'cadastrar', component: Cadastrar},
  { path: '', redirectTo: 'inicial', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
