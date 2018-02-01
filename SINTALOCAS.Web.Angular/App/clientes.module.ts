import { NgModule} from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClientesListaComponent } from './clientes-lista.component';
@NgModule({ 
    imports : [ 
        CommonModule 
    ],
     declarations : [
         ClientesListaComponent
     ],
     exports : [ ClientesListaComponent]
})
export class ClientesModule {}