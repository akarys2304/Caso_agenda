import { Component, OnInit, resolveForwardRef } from '@angular/core';
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
      Validators.minLength(12),
      Validators.maxLength(19)
    ],
      
    )
  });

  public telefoneLimpo: string = "";
  public valor: string = "";
  public aplicarMascaraTelefone(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (!input) return;

    this.valor = input.value.replace(/\D/g, "");
    this.telefoneLimpo = this.valor;

    if (this.valor.length > 0) {
      this.valor = "+" + this.valor;
    }
    if (this.valor.length > 3) {
      this.valor = this.valor.replace(/^\+(\d{2})(\d)/, "+$1 ($2");
    }
    if (this.valor.length > 7) {
      this.valor = this.valor.replace(/^\+(\d{2}) \((\d{2})(\d)/, "+$1 ($2) $3");
    }

    if (this.valor.length > 11) {
      this.valor = this.valor.replace(/(\d{4})(\d{4})$/, "$1-$2");
    } else if (this.valor.length > 12) {
      this.valor = this.valor.replace(/(\d{5})(\d{4})$/, "$1-$2");
    }

    input.value = this.valor;
  }

  onSubmit(){
     if (this.contatoForm.valid) {
      this.contatoForm.value.telefone = this.telefoneLimpo;
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
