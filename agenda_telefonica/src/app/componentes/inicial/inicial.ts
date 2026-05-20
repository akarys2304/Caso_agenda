import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-inicial',
  standalone: false,
  templateUrl: './inicial.html',
  styleUrl: './inicial.scss',
})

export class Inicial {

  constructor(private router: Router) {}

  mudarDeTela(){
    this.router.navigate(['/contatos.html'])
  }

}
