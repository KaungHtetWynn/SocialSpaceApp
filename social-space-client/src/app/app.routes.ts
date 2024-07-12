import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailsComponent } from './members/member-details/member-details.component';
import { authGuard } from './guards/auth.guard';

// Each of routes has a path which is used to match against
// These will be displayed at Router Outlet
// export const routes: Routes = [
//     { path: '', component: HomeComponent },
//     { path: 'members', component: MemberListComponent, canActivate: [authGuard] },
//     { path: 'members/:id', component: MemberDetailsComponent }, // :id is a dynamic route
//     { path: 'lists', component: HomeComponent },
//     { path: 'messages', component: HomeComponent },
//     { path: '**', component: HomeComponent, pathMatch: 'full' }, // wildcard - if none of the routes match
// ];


export const routes: Routes = [
    { path: '', component: HomeComponent },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [authGuard],
        children: [
            { path: 'members', component: MemberListComponent, canActivate: [authGuard] },
            { path: 'members/:id', component: MemberDetailsComponent }, // :id is a dynamic route
            { path: 'lists', component: HomeComponent },
            { path: 'messages', component: HomeComponent },
        ]
    },
    { path: '**', component: HomeComponent, pathMatch: 'full' }, // wildcard - if none of the routes match
];
