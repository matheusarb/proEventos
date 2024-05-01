import { Lote } from "./Lote";
import { Palestrante } from "./Palestrante";
import { RedeSocial } from "./RedeSocial";

export interface Evento {
    Id: number;
    Local: string;
    Data?: Date;
    Tema: string;
    QtdPessoas: number;
    ImagemURL: string;
    Telefone: string;
    Email: string;
    Lotes: Lote[];
    RedesSociais: RedeSocial[];
    PalestrantesEventos: Palestrante[];
}
