import { RouteInfo } from "../vertical-menu/vertical-menu.metadata";

export const HROUTES: RouteInfo[] = [
  {
    path: "/page", title: "Page", icon: "ft-home", class: "dropdown nav-item", isExternalLink: false, submenu: []
  },
  {
    path: "", title: "Catalogs", icon: "ft-align-left", class: "dropdown nav-item has-sub", badge: "", badgeClass: "", isExternalLink: false,
    submenu:
      [
        
        { path: "/Catalogs/Sucursal/List", title: "Sucursal", icon: "ft-arrow-right submenu-icon", class: "dropdown-item", badge: "", badgeClass: "", isExternalLink: false, submenu: [] },
        
        { path: "/Catalogs/Zona/List", title: "Zona", icon: "ft-arrow-right submenu-icon", class: "dropdown-item", badge: "", badgeClass: "", isExternalLink: false, submenu: [] },
        
        { path: "/Catalogs/Vendedor/List", title: "Vendedor", icon: "ft-arrow-right submenu-icon", class: "dropdown-item", badge: "", badgeClass: "", isExternalLink: false, submenu: [] },
        
        { path: "/Catalogs/PreContrato/List", title: "Pre contrato", icon: "ft-arrow-right submenu-icon", class: "dropdown-item", badge: "", badgeClass: "", isExternalLink: false, submenu: [] },
        
        { path: "/Catalogs/Contrato/List", title: "Contrato", icon: "ft-arrow-right submenu-icon", class: "dropdown-item", badge: "", badgeClass: "", isExternalLink: false, submenu: [] },
        
        { path: "/Catalogs/CatTipoCuenta/List", title: "Tipo de cuenta", icon: "ft-arrow-right submenu-icon", class: "dropdown-item", badge: "", badgeClass: "", isExternalLink: false, submenu: [] },
        
        { path: "/Catalogs/Cuenta/List", title: "Cuenta", icon: "ft-arrow-right submenu-icon", class: "dropdown-item", badge: "", badgeClass: "", isExternalLink: false, submenu: [] },
        
        { path: "/Catalogs/Cliente/List", title: "Cliente", icon: "ft-arrow-right submenu-icon", class: "dropdown-item", badge: "", badgeClass: "", isExternalLink: false, submenu: [] },
        
      ]
  },
  {
    path: "", title: "UserManagement", icon: "fa fa-users", class: "dropdown nav-item has-sub", badge: "", badgeClass: "", isExternalLink: false,
    submenu:
      [
        { path: "/UserManagement/Roles/List", title: "Roles", icon: "ft-arrow-right submenu-icon", class: "dropdown-item", badge: "", badgeClass: "", isExternalLink: false, submenu: [] },
        { path: "/UserManagement/Users/List", title: "Usuarios", icon: "ft-arrow-right submenu-icon", class: "dropdown-item", badge: "", badgeClass: "", isExternalLink: false, submenu: [] }
      ]
  }
];
