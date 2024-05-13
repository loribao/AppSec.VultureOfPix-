import { TrayIcon } from "@tauri-apps/api/tray";
import { Menu, MenuItem } from "@tauri-apps/api/menu";
import { WebviewWindow } from "@tauri-apps/api/webviewWindow";

export default async function Tray() {

    const createWindowSonar = async () => {
        {
            const webview = new WebviewWindow('sonar', {
                title: 'VulturePix - Sonar',
                parent: 'main',
                url: 'http://localhost:9000/projects/create'
            });
            webview.once('tauri://created', function () {
                console.log('webview created');
            });
            webview.once('tauri://error', function (e) {
                console.log('webview error', e);
            });
        }
    }
    const createWindowZap = async () => {
        {
            const webview = new WebviewWindow('zap', {
                title: 'VulturePix - Zap',
                parent: 'main',
                url: 'http://localhost:8080/zap'
            });
            webview.once('tauri://created', function () {
                console.log('webview created');
            });
            webview.once('tauri://error', function (e) {
                console.log('webview error', e);
            });
        }
    }
    const createWindowKibana = async () => {
        {
            const webview = new WebviewWindow('kibna', {
                parent: 'main',
                title: 'VulturePix - Kibana',
                url: 'https://localhost:5601/'
            });
            webview.once('tauri://created', function () {
                console.log('webview created');
            });
            webview.once('tauri://error', function (e) {
                console.log('webview error', e);
            });
        }
    }
    const createWindowAppSecGraphql = async () => {
        {
            const webview = new WebviewWindow('appsec', {
                parent: 'main',
                title: 'VulturePix - AppSec Graphql',
                url: 'http://localhost:5080/graphql/',
                contentProtected: false,

            });
            webview.once('tauri://created', function () {
                console.log('webview created');
            });
            webview.once('tauri://error', function (e) {
                console.log('webview error', e);
            });
        }
    }
    const createMenu = async () => {
        try {
            const tray = await TrayIcon.getById('appsec-tray');
            await tray?.setTitle('AppSec Desktop');
            await tray?.setMenuOnLeftClick(true);
            const menuItems = await MenuItem.new({
                text: 'SonarQube',
                action: createWindowSonar
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
                text: 'AppSec Graphql',
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

