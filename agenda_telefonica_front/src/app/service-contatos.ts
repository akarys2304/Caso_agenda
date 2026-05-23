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
  private apiUrl = 'http://localhost:8080/api/contatos';

  constructor(private http: HttpClient) { }

  // Método que realiza o GET
  obterContatos<T>(): Observable<T> {
    return this.http.get<T>(this.apiUrl);
  }

  apagarContato<T>(id: number): Observable<T> {
    return this.http.delete<T>(`${this.apiUrl}/${id}`);
  }
}
