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

  public telefoneLimpo: any;
  public telefoneVisivel: string = "";

  
  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.contatoId = Number(idParam);


      this.service.obterContatoPorId(this.contatoId).subscribe({
        next: (resposta: any) =>{
          this.contato = resposta;
          this.telefoneLimpo = resposta.telefone;
          this.telefoneVisivel = this.formatarTexto(this.telefoneLimpo);
          this.editaContatoForm.patchValue({
            nome: resposta.nome,
            telefone: this.telefoneVisivel
          })
        },
        error: (erro) => {
          console.error('Erro ao buscar o contato:', erro);
        }
      })
    }
  }

  public aplicarMascaraTelefone(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (!input) return;
    this.telefoneLimpo = input.value.replace(/\D/g, "");
    this.telefoneVisivel = this.formatarTexto(this.telefoneLimpo);
    input.value = this.telefoneVisivel;
  }

  private formatarTexto(valor: string): string {
    let textoFormatado = valor.replace(/\D/g, "");

    if (textoFormatado.length > 0) {
      textoFormatado = "+" + textoFormatado;
    }
    if (textoFormatado.length > 3) {
      textoFormatado = textoFormatado.replace(/^\+(\d{2})(\d)/, "+$1 ($2");
    }
    if (textoFormatado.length > 7) {
      textoFormatado = textoFormatado.replace(/^\+(\d{2}) \((\d{2})(\d)/, "+$1 ($2) $3");
    }
    if (textoFormatado.length > 11) {
      textoFormatado = textoFormatado.replace(/(\d{4})(\d{4})$/, "$1-$2");
    } else if (textoFormatado.length > 12) {
      textoFormatado = textoFormatado.replace(/(\d{5})(\d{4})$/, "$1-$2");
    }

    return textoFormatado;
  }


  editaContatoForm = new FormGroup({
    nome: new FormControl('', [Validators.required]),
    telefone: new FormControl('', [
      Validators.required, 
      Validators.minLength(13),
      Validators.maxLength(19)
    ],
      
    )
  });

  onSubmit(){
    if (confirm('Tem certeza que deseja atualizar este contato?')) {
      console.warn(this.editaContatoForm.value);
      this.editaContatoForm.value.telefone = this.telefoneLimpo;
      this.service.atualizarContato(this.contatoId, this.editaContatoForm.value).subscribe({
        next: (resposta) =>{
          console.log('Contato atualizado com sucesso!', resposta);
        },
        error: (erro) =>{
          console.error('Erro ao atualizar contato:', erro);
        }
      });
      // this.voltarAoInicio();
    }
  }

  voltarAoInicio(){
    this.router.navigate(['/contatos'])
  }

}
