import { AfterViewInit, ChangeDetectorRef, Component, OnDestroy, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Subscription } from "rxjs";
import { JwtService } from "../auth/jwt.service";
import Utils from "../helpers/Utils";
import { GenericResponse } from "../models/common/GenericResponse";
import { ConfigService } from "../services/config.service";
import { LayoutService } from "../services/layout.service";
import { MenuService } from "../services/syscore/menu.service";
import { RouteInfo } from "../vertical-menu/vertical-menu.metadata";

@Component({
  selector: "app-horizontal-menu",
  templateUrl: "./horizontal-menu.component.html",
  styleUrls: ["./horizontal-menu.component.scss"]
})
export class HorizontalMenuComponent implements OnInit, AfterViewInit, OnDestroy {

  public menuItems: any[];
  public config: any = {};
  level: number = 0;
  transparentBGClass = "";
  menuPosition = "Side";

  layoutSub: Subscription;

  private HROUTES: any[] = [];

  constructor(private layoutService: LayoutService,
    private configService: ConfigService,
    private cdr: ChangeDetectorRef,
    private router: Router,
    private jwtService: JwtService,
    private menuService: MenuService) {
    this.config = this.configService.templateConf;
  }

  ngOnInit() {
    const jwt = this.jwtService.get();
    this.menuService.getMenuByUser(jwt.id).subscribe((response: GenericResponse<any>) => {
      if (response &&
        response.status === 1 &&
        Utils.IsValidArray(response.data)) {
        this.HROUTES = this.generateHorizontalMenuRecursively(response.data, { class: "dropdown nav-item has-sub" });
        this.menuItems = this.HROUTES;
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

  ngAfterViewInit() {

    this.layoutSub = this.configService.templateConf$.subscribe((templateConf) => {
      if (templateConf) {
        this.config = templateConf;
      }
      this.loadLayout();
      //this.cdr.markForCheck();
      this.cdr.detectChanges();
    })
  }

  loadLayout() {
    if (this.config.layout.menuPosition && this.config.layout.menuPosition.toString().trim() != "") {
      this.menuPosition = this.config.layout.menuPosition;
    }


    if (this.config.layout.variant === "Transparent") {
      this.transparentBGClass = this.config.layout.sidebar.backgroundColor;
    }
    else {
      this.transparentBGClass = "";
    }
  }

  ngOnDestroy() {
    if (this.layoutSub) {
      this.layoutSub.unsubscribe();
    }
  }
}
