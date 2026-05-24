import { Component, resolveForwardRef } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ServiceContatos } from '../../service-contatos';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-cadastrar',
  standalone: false,
  templateUrl: './cadastrar.html',
  styleUrl: './cadastrar.scss',
})

export class Cadastrar {
  constructor(private router: Router, private service: ServiceContatos, private route: ActivatedRoute) {}

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
     if (this.contatoForm.valid) {
      this.service.cadastrarContato(this.contatoForm.value).subscribe({
        next: (resposta) =>{
          console.log('Contato criado com sucesso', resposta);
          this.router.navigate(['/contatos'])
        },
        error: (erro) =>{
          console.error('Erro ao criar contato', erro);
        }
      })
    }
    else {
      console.warn('Formulário inválido. Verifique os campos.');
    }
  }

}
