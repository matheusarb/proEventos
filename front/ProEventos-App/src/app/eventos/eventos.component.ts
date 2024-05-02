import { Component, OnInit } from '@angular/core';
import { EventoService } from '../services/evento.service';
import { Evento } from '../models/Evento';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
//   providers: [EventoService]
})

export class EventosComponent implements OnInit {
  public eventos: Evento[] = [];
  public eventosFiltrados: Evento[] = [];
  public larguraImg: number = 100;
  public margemImg: number = 2;
  public exibirImg: boolean = true;
  private _filtroLista: string = '';

  constructor(private eventoService: EventoService) {}

  public get filtroLista(){
    return this._filtroLista;
  }

  public set filtroLista(value: string){
    this._filtroLista = value;
    this.eventosFiltrados = this._filtroLista ? this.filtrarEventos(this._filtroLista) : this.eventos;
  }

  public filtrarEventos(filtrarPor: string): Evento[]{
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter((evento: {tema:string, local:string}) =>
        evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
        evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  public ngOnInit(): void {
    this.getEventos();
  }

  public toggleImg(){
    this.exibirImg = !this.exibirImg;
  }

  public getEventos(): void {
    this.eventoService.getEventos().subscribe(
        {
            next: (eventosResp: Evento[]) => {
                this.eventos = eventosResp;
                this.eventosFiltrados = this.eventos;
            },
            error: (error: any) => console.log(error)
        }
    );
  }
}