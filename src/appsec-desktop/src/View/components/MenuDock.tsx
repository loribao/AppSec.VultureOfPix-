import React, { useState } from "react";
import "./MenuDock.css";
import arrow from "../assets/img/arrow.svg";
import zap from "../assets/img/zap.jpg";
import sonar from "../assets/img/sonar.png";
import elastic from "../assets/img/elastic.png";
import bananagraphql from '../assets/img/bananagraphql.svg';
import {createWindowAppSecGraphql,createWindowKibana,createWindowSonar,createWindowZap} from '../../Data/contexts/Tray';
export default function MenuDock() {
    const [menuOpen, setMenuOpen] = useState(false);
    const [arrowRotation, setArrowRotation] = useState(0);

    const toggleMenu = () => {
        setMenuOpen(!menuOpen);
        setArrowRotation(arrowRotation === 0 ? 180 : 0);
    };
    return (
        <div className={`mainMenuDockContainer ${menuOpen ? "open" : ""}`}>
            <div
                className={`openCloseMenuDock ${menuOpen ? "" : "closed"}`}
                onClick={toggleMenu}
            >
                <img
                    src={arrow}
                    alt=""
                    style={{ transform: `rotate(${arrowRotation}deg)` }}
                />
            </div>
            {menuOpen && (
                <div className="menuDockContainer">
                    <div className="appMenuDockContainer">
                        <a className="appOption" href="#">
                            <img src={zap} alt=""  onClick={createWindowZap} />
                        </a>
                        <a className="appOption" href="#">
                            <img src={sonar} alt=""  onClick={createWindowSonar} />
                        </a>
                        <a className="appOption" href="#">
                            <img src={elastic} alt=""  onClick={createWindowKibana}/>
                        </a>
                        <a className="appOption" href="#">
                            <img style={{width: '30',height:  '30'}} src={bananagraphql} alt=""  onClick={createWindowAppSecGraphql} />
                        </a>
                    </div>
                </div>
            )}
        </div>
    );
}
