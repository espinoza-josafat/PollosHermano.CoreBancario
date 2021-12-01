import {
    AfterViewInit, ChangeDetectorRef, Component,
    ElementRef, HostListener, OnDestroy, OnInit, ViewChild
} from "@angular/core";

import { Router } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";
import { DeviceDetectorService } from "ngx-device-detector";
import { Subscription } from "rxjs";
import { customAnimations } from "../animations/custom-animations";
import { JwtService } from "../auth/jwt.service";
import Utils from "../helpers/Utils";
import { GenericResponse } from "../models/common/GenericResponse";
import { ConfigService } from "../services/config.service";
import { LayoutService } from "../services/layout.service";
import { MenuService } from "../services/syscore/menu.service";
import { RouteInfo } from "./vertical-menu.metadata";

@Component({
  selector: "app-sidebar",
  templateUrl: "./vertical-menu.component.html",
  styleUrls: ["./vertical-menu.component.scss"],
  animations: customAnimations
})
export class VerticalMenuComponent implements OnInit, AfterViewInit, OnDestroy {

  @ViewChild("toggleIcon") toggleIcon: ElementRef;
  public menuItems: any[];
  level: number = 0;
  logoUrl = "assets/img/logo.png";
  public config: any = {};
  protected innerWidth: any;
  layoutSub: Subscription;
  configSub: Subscription;
  perfectScrollbarEnable = true;
  collapseSidebar = false;
  resizeTimeout;
  private ROUTES: any[] = [];
  private HROUTES: any[] = [];

  constructor(
    private router: Router,
    public translate: TranslateService,
    private layoutService: LayoutService,
    private configService: ConfigService,
    private cdr: ChangeDetectorRef,
    private deviceService: DeviceDetectorService,
    private jwtService: JwtService,
    private menuService: MenuService
  ) {
    this.config = this.configService.templateConf;
    this.innerWidth = window.innerWidth;
    this.isTouchDevice();
  }

  ngOnInit() {
    const jwt = this.jwtService.get();
    this.menuService.getMenuByUser(jwt.id).subscribe((response: GenericResponse<any>) => {
      if (response &&
        response.status === 1 &&
        Utils.IsValidArray(response.data)) {
        this.HROUTES = this.generateHorizontalMenuRecursively(response.data, { class: "dropdown nav-item has-sub" });
        this.ROUTES = this.generateVerticalMenuRecursively(response.data, { class: "has-sub", badgeClass: "badge badge-pill badge-danger float-right mr-1 mt-1" });

        this.menuItems = this.ROUTES;
      }
    },
      (error: any) => {

      },
      () => {

      });
  }

  private generateHorizontalMenuRecursively(menus: any[], complement: any): RouteInfo[] {
    const result: RouteInfo[] = [];

    for (var i = 0; i < menus.length; i++) {
      const menu = menus[i];
      result.push({
        path: menu.path,
        title: menu.name,
        icon: menu.icon,
        class: complement.class,
        isExternalLink: menu.isExternalLink,
        submenu: this.generateHorizontalMenuRecursively(menu.childrens, { class: "dropdown-item" }),
        badge: Utils.IsValidStringNotWhiteSpace(complement.badge) ? complement.badge : "",
        badgeClass: Utils.IsValidStringNotWhiteSpace(complement.badgeClass) ? complement.badgeClass : "",
      });
    }

    return result;
  }

  private generateVerticalMenuRecursively(menus: any[], complement: any): RouteInfo[] {
    const result: RouteInfo[] = [];

    for (var i = 0; i < menus.length; i++) {
      const menu = menus[i];
      result.push({
        path: menu.path,
        title: menu.name,
        icon: menu.icon,
        class: complement.class,
        isExternalLink: menu.isExternalLink,
        submenu: this.generateVerticalMenuRecursively(menu.childrens, { class: "" }),
        badge: Utils.IsValidStringNotWhiteSpace(complement.badge) ? complement.badge : "",
        badgeClass: Utils.IsValidStringNotWhiteSpace(complement.badgeClass) ? complement.badgeClass : "",
      });
    }

    return result;
  }

  ngAfterViewInit() {
    this.configSub = this.configService.templateConf$.subscribe((templateConf) => {
      if (templateConf) {
        this.config = templateConf;
      }
      this.loadLayout();
      this.cdr.markForCheck();
    });

    this.layoutSub = this.layoutService.overlaySidebarToggle$.subscribe(
      collapse => {
        if (this.config.layout.menuPosition === "Side") {
          this.collapseSidebar = collapse;
        }
      });
  }

  @HostListener("window:resize", ["$event"])
  onWindowResize(event) {
    if (this.resizeTimeout) {
      clearTimeout(this.resizeTimeout);
    }
    this.resizeTimeout = setTimeout((() => {
      this.innerWidth = event.target.innerWidth;
      this.loadLayout();
    }).bind(this), 500);
  }

  loadLayout() {
    if (this.config.layout.menuPosition === "Top") { // Horizontal Menu
      if (this.innerWidth < 1200) { // Screen size < 1200
        this.menuItems = this.HROUTES;
      }
    }
    else if (this.config.layout.menuPosition === "Side") { // Vertical Menu{
      this.menuItems = this.ROUTES;
    }




    if (this.config.layout.sidebar.backgroundColor === "white") {
      this.logoUrl = "assets/img/logo-dark.png";
    }
    else {
      this.logoUrl = "assets/img/logo.png";
    }

    if (this.config.layout.sidebar.collapsed) {
      this.collapseSidebar = true;
    }
    else {
      this.collapseSidebar = false;
    }
  }

  toggleSidebar() {
    let conf = this.config;
    conf.layout.sidebar.collapsed = !this.config.layout.sidebar.collapsed;
    this.configService.applyTemplateConfigChange({ layout: conf.layout });

    setTimeout(() => {
      this.fireRefreshEventOnWindow();
    }, 300);
  }

  fireRefreshEventOnWindow = function () {
    const evt = document.createEvent("HTMLEvents");
    evt.initEvent("resize", true, false);
    window.dispatchEvent(evt);
  };

  CloseSidebar() {
    this.layoutService.toggleSidebarSmallScreen(false);
  }

  isTouchDevice() {
    const isMobile = this.deviceService.isMobile();
    const isTablet = this.deviceService.isTablet();

    if (isMobile || isTablet) {
      this.perfectScrollbarEnable = false;
    }
    else {
      this.perfectScrollbarEnable = true;
    }
  }

  ngOnDestroy() {
    if (this.layoutSub) {
      this.layoutSub.unsubscribe();
    }
    if (this.configSub) {
      this.configSub.unsubscribe();
    }
  }
}
