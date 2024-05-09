import React, { useState } from "react";
import "./MenuDock.css";
import zap from "../assets/img/zap.jpg";
import arrow from "../assets/img/arrow.svg";

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
                            <img src={zap} alt="" />
                        </a>
                        <a className="appOption" href="#">
                            <img src={zap} alt="" />
                        </a>
                        <a className="appOption" href="#">
                            <img src={zap} alt="" />
                        </a>
                    </div>
                </div>
            )}
        </div>
    );
}
