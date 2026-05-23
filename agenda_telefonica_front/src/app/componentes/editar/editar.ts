import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-editar',
  standalone: false,
  templateUrl: './editar.html',
  styleUrl: './editar.scss',
})
export class Editar {
  editaContatoForm = new FormGroup({
    nome: new FormControl('', [Validators.required]),
    telefone: new FormControl('', [
      Validators.required, 
      Validators.minLength(8),
      Validators.maxLength(9)
    ],
      
    )
  });

  onSubmit(){
    console.warn(this.editaContatoForm.value.nome)
  }

}
