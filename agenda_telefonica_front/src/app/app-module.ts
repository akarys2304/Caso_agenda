import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { Inicial } from './componentes/inicial/inicial';
import { Contatos } from './componentes/contatos/contatos';
import { Cadastrar } from './componentes/cadastrar/cadastrar';
import { Editar } from './componentes/editar/editar';
import { ReactiveFormsModule} from '@angular/forms';

@NgModule({
  declarations: [App, Inicial, Contatos, Cadastrar, Editar],
  imports: [BrowserModule, AppRoutingModule, ReactiveFormsModule],
  providers: [provideBrowserGlobalErrorListeners()],
  bootstrap: [App],
})
export class AppModule {}
