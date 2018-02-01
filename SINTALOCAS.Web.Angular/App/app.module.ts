import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { ClientesModule } from './clientes/clientes.module';

@NgModule({
    imports: [BrowserModule, ClientesModule],
    declarations: [AppComponent],
    bootstrap: [AppComponent]
})
export class AppModule { }  