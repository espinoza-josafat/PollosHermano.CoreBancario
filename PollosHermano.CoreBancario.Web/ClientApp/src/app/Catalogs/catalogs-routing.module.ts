import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

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


const routes: Routes = [
  {
    path: "",
    children: [


      {
        path: "Sucursal/List",
        component: SucursalListComponent,
        data: {
          title: "list"
        }
      },
      {
        path: "Sucursal/Edit",
        component: SucursalEditComponent,
        data: {
          title: "edit"
        }
      },
      {
        path: "Sucursal/Edit/:id",
        component: SucursalEditComponent,
        data: {
          title: "edit"
        }
      },

      {
        path: "Zona/List",
        component: ZonaListComponent,
        data: {
          title: "list"
        }
      },
      {
        path: "Zona/Edit",
        component: ZonaEditComponent,
        data: {
          title: "edit"
        }
      },
      {
        path: "Zona/Edit/:id",
        component: ZonaEditComponent,
        data: {
          title: "edit"
        }
      },

      {
        path: "Vendedor/List",
        component: VendedorListComponent,
        data: {
          title: "list"
        }
      },
      {
        path: "Vendedor/Edit",
        component: VendedorEditComponent,
        data: {
          title: "edit"
        }
      },
      {
        path: "Vendedor/Edit/:id",
        component: VendedorEditComponent,
        data: {
          title: "edit"
        }
      },

      {
        path: "PreContrato/List",
        component: PreContratoListComponent,
        data: {
          title: "list"
        }
      },
      {
        path: "PreContrato/Edit",
        component: PreContratoEditComponent,
        data: {
          title: "edit"
        }
      },
      {
        path: "PreContrato/Edit/:id",
        component: PreContratoEditComponent,
        data: {
          title: "edit"
        }
      },

      {
        path: "Contrato/List",
        component: ContratoListComponent,
        data: {
          title: "list"
        }
      },
      {
        path: "Contrato/Edit",
        component: ContratoEditComponent,
        data: {
          title: "edit"
        }
      },
      {
        path: "Contrato/Edit/:id",
        component: ContratoEditComponent,
        data: {
          title: "edit"
        }
      },

      {
        path: "CatTipoCuenta/List",
        component: CatTipoCuentaListComponent,
        data: {
          title: "list"
        }
      },
      {
        path: "CatTipoCuenta/Edit",
        component: CatTipoCuentaEditComponent,
        data: {
          title: "edit"
        }
      },
      {
        path: "CatTipoCuenta/Edit/:id",
        component: CatTipoCuentaEditComponent,
        data: {
          title: "edit"
        }
      },

      {
        path: "Cliente/List",
        component: ClienteListComponent,
        data: {
          title: "list"
        }
      },
      {
        path: "Cliente/Edit",
        component: ClienteEditComponent,
        data: {
          title: "edit"
        }
      },
      {
        path: "Cliente/Edit/:id",
        component: ClienteEditComponent,
        data: {
          title: "edit"
        }
      },

      {
        path: "Cuenta/List",
        component: CuentaListComponent,
        data: {
          title: "list"
        }
      },
      {
        path: "Cuenta/Edit",
        component: CuentaEditComponent,
        data: {
          title: "edit"
        }
      },
      {
        path: "Cuenta/Edit/:id",
        component: CuentaEditComponent,
        data: {
          title: "edit"
        }
      },

      
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CatalogsRoutingModule { }
