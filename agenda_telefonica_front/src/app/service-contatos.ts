import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Contatos } from './componentes/contatos/contatos';

export interface Contato {
  id: number;
  nome: string;
  telefone: string;
}

@Injectable({
  providedIn: 'root',
})
export class ServiceContatos {
  private readonly apiUrl = 'http://localhost:5189/api/contatos';

  constructor(private readonly http: HttpClient) { }

  // Método que realiza o GET
  obterContatos() {
    return this.http.get<any>(this.apiUrl);
  }

  //método DELETE
  apagarContato(id: number){
     return this.http.delete(`${this.apiUrl}/${id}`);
  }

  //GET por id
  obterContatoPorId(id: number){
    return this.http.get(`${this.apiUrl}/${id}`);
  }

  //PUT
  atualizarContato(id: number, dados: any): Observable<any>{ 
    return this.http.put<any>(`${this.apiUrl}/${id}`, dados)
  }

  //POST
  cadastrarContato(contato: any): Observable<any>{
    return this.http.post<any>(this.apiUrl, contato);
  }
}
