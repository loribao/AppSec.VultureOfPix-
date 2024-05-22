import { TrayIcon } from "@tauri-apps/api/tray";
import { Menu, MenuItem } from "@tauri-apps/api/menu";
import { WebviewWindow } from "@tauri-apps/api/webviewWindow";
export const createWindowSonar = (_id: string) => {
    const webview = new WebviewWindow('sonar', {
        title: 'VulturePix - Sonar',
        parent: 'main',
        url: 'http://localhost:9000/projects/create',
        center: true,
    });
    webview.once('tauri://created', function () {
        console.log('webview created');
    });
    webview.once('tauri://error', function (e) {
        console.log('webview error', e);
    });
}
export const createWindowZap = (_id: string) => {
    const webview = new WebviewWindow('zap', {
        title: 'VulturePix - Zap',
        parent: 'main',
        url: 'http://localhost:8080/zap',
        center: true,
    });
    webview.once('tauri://created', function () {
        console.log('webview created');
    });
    webview.once('tauri://error', function (e) {
        console.log('webview error', e);
    });
}
export const createWindowKibana = (_id: string) => {

    const webview = new WebviewWindow('kibna', {
        parent: 'main',
        title: 'VulturePix - Kibana',
        url: 'https://localhost:5601/',
        center: true,
    });
    webview.once('tauri://created', function () {
        console.log('webview created');
    });
    webview.once('tauri://error', function (e) {
        console.log('webview error', e);
    });

}
export const createWindowAppSecGraphql = (_id: string) => {
    const webview = new WebviewWindow('appsec', {
        parent: 'main',
        title: 'VulturePix - AppSec API',
        url: 'http://localhost:5080/graphql/',
        contentProtected: false,
        center: true,
    });
    webview.once('tauri://created', function () {
        console.log('webview created');
    });
    webview.once('tauri://error', function (e) {
        console.log('webview error', e);
    });
}
export const createCodeSpace = (_id: string) => {
    const webview = new WebviewWindow('CodeSpace', {
        title: 'VulturePix - Code SPACE',
        parent: 'main',
        url: 'http://localhost:8443/?folder=/config/workspace',
        contentProtected: false,
        center: true,
        focus: true,
    });
    webview.once('tauri://created', function () {
        console.log('webview created');
    });
    webview.once('tauri://error', function (e) {
        console.log('webview error', e);
    });
}
export default async function Tray() {


    const createMenu = async () => {
        try {
            const tray = await TrayIcon.getById('appsec-tray');
            await tray?.setTitle('AppSec Desktop');
            await tray?.setMenuOnLeftClick(true);
            const menuItems = await MenuItem.new({
                text: 'SonarQube',
                action: createWindowSonar,
            })
            const menuItems2 = await MenuItem.new({
                text: 'Zap',
                action: createWindowZap,
            })
            const menuItems3 = await MenuItem.new({
                text: 'Kibana',
                action: createWindowKibana,
            })
            const menuItems4 = await MenuItem.new({
                text: 'AppSec API',
                action: createWindowAppSecGraphql,
            })
            let menu = await Menu.new({ items: [menuItems, menuItems2, menuItems3, menuItems4] });
            await tray?.setMenu(menu);
            await tray?.setIconAsTemplate(true);
            return true
        } catch (error) {
            return false
        }
    }
    return createMenu();
}

