import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

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
}
