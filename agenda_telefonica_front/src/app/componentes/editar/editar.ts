import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import {ServiceContatos } from '../../service-contatos';

@Component({
  selector: 'app-editar',
  standalone: false,
  templateUrl: './editar.html',
  styleUrl: './editar.scss',
})
export class Editar implements OnInit{

  contato: any = { nome: '', telefone: '' };
  contatoId!: number;

  constructor(private router: Router, private service: ServiceContatos, private route: ActivatedRoute) {}

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.contatoId = Number(idParam);


      this.service.obterContatoPorId(this.contatoId).subscribe({
        next: (resposta: any) =>{
          this.contato = resposta;
          this.editaContatoForm.patchValue({
            nome: resposta.nome,
            telefone: resposta.telefone
          })
        },
        error: (erro) => {
          console.error('Erro ao buscar o contato:', erro);
        }
      })
    }
  }

  editaContatoForm = new FormGroup({
    nome: new FormControl('', [Validators.required]),
    telefone: new FormControl('', [
      Validators.required, 
      Validators.minLength(13),
      Validators.maxLength(14)
    ],
      
    )
  });

  onSubmit(){
    console.warn(this.editaContatoForm.value);
    this.service.atualizarContato(this.contatoId, this.editaContatoForm.value).subscribe({
      next: (resposta) =>{
        console.log('Contato atualizado com sucesso!', resposta);
      },
      error: (erro) =>{
         console.error('Erro ao atualizar contato:', erro);
      }
    });
  }

  voltarAoInicio(){
    this.router.navigate(['/contatos'])
  }

}
