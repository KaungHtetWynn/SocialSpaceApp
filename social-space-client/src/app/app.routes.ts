import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailsComponent } from './members/member-details/member-details.component';
import { authGuard } from './_guards/auth.guard';
import { ErrorTestComponent } from './errors/error-test/error-test.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';

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
            { path: 'members/:username', component: MemberDetailsComponent }, // :id or username is a dynamic route (route parameter)
            { path: 'lists', component: HomeComponent },
            { path: 'messages', component: HomeComponent },
            // { path: 'error-test', component: ErrorTestComponent },
            // { path: 'not-found', component: NotFoundComponent },
            // { path: 'server-error', component: ServerErrorComponent },
        ]
    },
    { path: 'error-test', component: ErrorTestComponent },
    { path: 'not-found', component: NotFoundComponent },
    { path: 'server-error', component: ServerErrorComponent },
    { path: '**', component: HomeComponent, pathMatch: 'full' }, // wildcard - if none of the routes match
];
