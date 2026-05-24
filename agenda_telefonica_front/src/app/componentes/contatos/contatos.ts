import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
import { Contato, ServiceContatos } from '../../service-contatos';

@Component({
  selector: 'app-contatos',
  standalone: false,
  templateUrl: './contatos.html',
  styleUrl: './contatos.scss',
}as any)
export class Contatos implements OnInit{
  constructor(private router: Router, private service: ServiceContatos, private cdRef: ChangeDetectorRef ) {}
  ngOnInit(): void {
    this.carregarContatos();
  }

  // contatos: Contato[] = []
  contatos: any;
  zeroContatos: boolean = true;
  carregarContatos() {
    this.service.obterContatos().subscribe({
      next: (resposta: any) => {
        // setTimeout(() => {
          this.contatos = resposta; 
          this.zeroContatos = this.contatos.length === 0;
          this.cdRef.detectChanges();
        // });
      },
      error: (erro) => {
        console.error('Erro ao buscar dados:', erro)
      }
    });
  }

  formatarTexto(valor: string): string {
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

  cadastrarContato(){
    this.router.navigate(['/cadastrar'])
  }

  editarContato(id: number){
    this.router.navigate(['/editar', id]); 
  }

  excluirContato(id: number) : void{
    if (confirm('Tem certeza que deseja excluir este contato?')) {
      this.service.apagarContato(id).subscribe({
        next: () => {
          console.log('Deletado com sucesso!');
          this.carregarContatos()
        },
        error: (err) => console.error('Erro ao apagar contato:', err)
      })
    }
  }

  enviarWhatsApp(tel: any){
    var url = "https://wa.me/"+tel
    window.open(url, '_blank')
  }

  voltarAoInicio(){
    this.router.navigate(['/inicial'])
  }
}
