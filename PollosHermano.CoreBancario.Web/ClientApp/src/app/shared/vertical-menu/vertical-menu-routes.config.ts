import { RouteInfo } from "./vertical-menu.metadata";

//Sidebar menu Routes and data
export const ROUTES: RouteInfo[] = [
  {
    path: "/page", title: "Page", icon: "ft-home", class: "", badge: "", badgeClass: "", isExternalLink: false, submenu: []
  },
  {
    path: "", title: "Catalogs", icon: "ft-align-left", class: "has-sub", badge: "", badgeClass: "badge badge-pill badge-danger float-right mr-1 mt-1", isExternalLink: false,
    submenu:
      [
        
        { path: "/Catalogs/Sucursal/List", title: "Sucursal", icon: "ft-arrow-right submenu-icon", class: "", badge: "", badgeClass: "", isExternalLink: false, submenu: [] },
        
        { path: "/Catalogs/Zona/List", title: "Zona", icon: "ft-arrow-right submenu-icon", class: "", badge: "", badgeClass: "", isExternalLink: false, submenu: [] },
        
        { path: "/Catalogs/Vendedor/List", title: "Vendedor", icon: "ft-arrow-right submenu-icon", class: "", badge: "", badgeClass: "", isExternalLink: false, submenu: [] },
        
        { path: "/Catalogs/PreContrato/List", title: "Pre contrato", icon: "ft-arrow-right submenu-icon", class: "", badge: "", badgeClass: "", isExternalLink: false, submenu: [] },
        
        { path: "/Catalogs/Contrato/List", title: "Contrato", icon: "ft-arrow-right submenu-icon", class: "", badge: "", badgeClass: "", isExternalLink: false, submenu: [] },
        
        { path: "/Catalogs/CatTipoCuenta/List", title: "Tipo de cuenta", icon: "ft-arrow-right submenu-icon", class: "", badge: "", badgeClass: "", isExternalLink: false, submenu: [] },
        
        { path: "/Catalogs/Cliente/List", title: "Cliente", icon: "ft-arrow-right submenu-icon", class: "", badge: "", badgeClass: "", isExternalLink: false, submenu: [] },
        
        { path: "/Catalogs/Cuenta/List", title: "Cuenta", icon: "ft-arrow-right submenu-icon", class: "", badge: "", badgeClass: "", isExternalLink: false, submenu: [] },
        
      ]
  },
  {
    path: "", title: "UserManagement", icon: "fa fa-users", class: "has-sub", badge: "", badgeClass: "badge badge-pill badge-danger float-right mr-1 mt-1", isExternalLink: false,
    submenu:
      [
        { path: "/UserManagement/Roles/List", title: "Roles", icon: "ft-arrow-right submenu-icon", class: "", badge: "", badgeClass: "", isExternalLink: false, submenu: [] },
        { path: "/UserManagement/Users/List", title: "Usuarios", icon: "ft-arrow-right submenu-icon", class: "", badge: "", badgeClass: "", isExternalLink: false, submenu: [] },
      ]
  }
];
