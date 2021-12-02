import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";

import { NgxDatatableModule } from "@swimlane/ngx-datatable";

import { CatalogsRoutingModule } from "./catalogs-routing.module";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { CustomFormsModule } from "ngx-custom-validators";
import { ArchwizardModule } from "angular-archwizard";
import { UiSwitchModule } from "ngx-ui-switch";
import { NgSelectModule } from "@ng-select/ng-select";
import { TagInputModule } from "ngx-chips";
import { QuillModule } from "ngx-quill";
import { MatchHeightModule } from "../shared/directives/match-height.directive";

import { CustomComponentsModule } from "../custom/custom-components.module";

import { SucursalListComponent } from "./Sucursal/List/sucursal-list.component";
import { SucursalEditComponent } from "./Sucursal/Edit/sucursal-edit.component";
import { ZonaListComponent } from "./Zona/List/zona-list.component";
import { ZonaEditComponent } from "./Zona/Edit/zona-edit.component";
import { VendedorListComponent } from "./Vendedor/List/vendedor-list.component";
import { VendedorEditComponent } from "./Vendedor/Edit/vendedor-edit.component";
import { PreContratoListComponent } from "./PreContrato/List/pre-contrato-list.component";
import { PreContratoEditComponent } from "./PreContrato/Edit/pre-contrato-edit.component";
import { ContratoListComponent } from "./Contrato/List/contrato-list.component";
import { ContratoEditComponent } from "./Contrato/Edit/contrato-edit.component";
import { CatTipoCuentaListComponent } from "./CatTipoCuenta/List/cat-tipo-cuenta-list.component";
import { CatTipoCuentaEditComponent } from "./CatTipoCuenta/Edit/cat-tipo-cuenta-edit.component";
import { ClienteListComponent } from "./Cliente/List/cliente-list.component";
import { ClienteEditComponent } from "./Cliente/Edit/cliente-edit.component";
import { CuentaListComponent } from "./Cuenta/List/cuenta-list.component";
import { CuentaEditComponent } from "./Cuenta/Edit/cuenta-edit.component";


@NgModule({
  imports: [
    CommonModule,
    NgxDatatableModule,
    CatalogsRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    ArchwizardModule,
    CustomFormsModule,
    MatchHeightModule,
    NgbModule,
    UiSwitchModule,
    QuillModule.forRoot(),
    NgSelectModule,
    TagInputModule,
    CustomComponentsModule
  ],
  declarations: [

    SucursalListComponent,
    SucursalEditComponent,
    ZonaListComponent,
    ZonaEditComponent,
    VendedorListComponent,
    VendedorEditComponent,
    PreContratoListComponent,
    PreContratoEditComponent,
    ContratoListComponent,
    ContratoEditComponent,
    CatTipoCuentaListComponent,
    CatTipoCuentaEditComponent,
    ClienteListComponent,
    ClienteEditComponent,
    CuentaListComponent,
    CuentaEditComponent,


  ]
})
export class CatalogsModule { }
