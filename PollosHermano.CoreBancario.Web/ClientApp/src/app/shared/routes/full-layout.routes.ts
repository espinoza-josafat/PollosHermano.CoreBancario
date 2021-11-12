import { Routes, RouterModule } from '@angular/router';

//Route for content layout with sidebar, navbar and footer.

export const Full_ROUTES: Routes = [
  {
    path: 'page',
    loadChildren: () => import('../../page/page.module').then(m => m.PageModule)
  },
  {
    path: 'Catalogs',
    loadChildren: () => import('../../Catalogs/catalogs.module').then(m => m.CatalogsModule)
  },
  {
    path: "UserManagement",
    loadChildren: () => import("../../UserManagement/user-management.module").then(m => m.UserManagementModule)
  }
];
