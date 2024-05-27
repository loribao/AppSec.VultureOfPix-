import { useEffect, useState } from "react";
import "./MenuDock.css";
import arrow from "../assets/img/arrow.svg";
import { WebviewWindow } from "@tauri-apps/api/webviewWindow";
import { Window } from '@tauri-apps/api/window';
export const createWindow = (url: string, title: string, name: string) => {
    const webview = new WebviewWindow(name, {
        theme: 'dark',
        title: title,
        url: new URL(url).toString(),
        center: true
    });
    webview.once('tauri://created', function () {
        console.log('webview created');
    });
    webview.once('tauri://error', function (e) {
        console.log('webview error', e);
    });
}
import { invoke } from '@tauri-apps/api/core';
export default function MenuDock() {
    const [menuOpen, setMenuOpen] = useState(true);
    const [arrowRotation, setArrowRotation] = useState(0);
    const [serverBaseUrl, setServerBaseUrl] = useState(new URL("https://localhost:5081"));
    const [apps, setApps] = useState(
        [{
            name: "",
            url: "",
            title: "",
            description: "",
            image: "",
            show: false,
        }]
    );
    useEffect(() => {
        invoke("url_server").then((response) => {
            const url = new URL(response as string);
            setServerBaseUrl(url);

            fetch(new URL("/graphql", url), {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    query: `query {urls {description,image,show,title,url,name}}`
                })
            }).then(response => response.json()
            ).then(data => {
                setApps(data.data.urls);
            })
        });
    }, []);
    const toggleMenu = () => {
        setMenuOpen(!menuOpen);
        setArrowRotation(arrowRotation === 0 ? 180 : 0);
    };
    return (

            <div className="appMenuDockContainer" data-tauri-drag-region>
                {apps.filter(app => app.show).map((app, index) => {
                    const image = new URL(app.image, serverBaseUrl);
                    return (
                        <button
                            key={app.name}
                            className="appOption"
                            onClick={() => createWindow(app.url, app.title, app.name)}
                            aria-label={app.name}
                        >
                            <img src={image.toString()} alt={app.name} />
                        </button>
                    )
                })}
            </div>
    );
}
