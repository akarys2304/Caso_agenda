import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-cadastrar',
  standalone: false,
  templateUrl: './cadastrar.html',
  styleUrl: './cadastrar.scss',
})

export class Cadastrar {
  contatoForm = new FormGroup({
    nome: new FormControl('', [Validators.required]),
    telefone: new FormControl('', [
      Validators.required, 
      Validators.minLength(13),
      Validators.maxLength(13)
    ],
      
    )
  });

  onSubmit(){
    console.warn(this.contatoForm.value.nome)
  }
}
