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
  zeroContatos: boolean = false
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

  //Conjunto temporário
  dados: Array<{ id: number, nome: string, telefone: string }> = [
    { id: 1, nome: 'Aline', telefone: "5527999990000" },
    { id: 2, nome: 'Karys', telefone: "5516999990000" },
    { id: 3, nome: 'Pessoa3', telefone: "5521999990000" },
    { id: 4, nome: 'Pessoa4', telefone: "5521999990000" },
    { id: 5, nome: 'Pessoa5', telefone: "5521999990000" },
    { id: 6, nome: 'Pessoa6', telefone: "5521999990000" }
  ];

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
}
