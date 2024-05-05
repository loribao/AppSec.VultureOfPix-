import { useRef, useState } from "react";
import "../assets/css/projectPage.css";
const Projetos = () => {
    const dropbtnRef = useRef<HTMLButtonElement>(null);
    const dropdownContentRef = useRef<HTMLDivElement>(null);
    const [show, setShow] = useState(false);
    const handleClick = () => {
        if (dropbtnRef.current && dropdownContentRef.current) {
            setShow(!show);
            const rect = dropbtnRef.current.getBoundingClientRect();
            dropdownContentRef.current.style.bottom = `${window.innerHeight - rect.top}px`;
        }
    };
    return (
        <div className="bodyContainer">
            <header className="headerContainer">
                <a className="logo" href="#"
                ><img src="./assets/img/logo.svg" alt=""
                    /></a>
                <nav className="navContainer">
                    <div className="avatarContainer" id="avatarDropdown">
                        <div className="avatar"></div>
                        <div className="dropDownContent">
                            <a href="./myProfile.html">Meu Perfil</a>
                            <a href="./forgotPassword.html">Alterar Senha</a>
                            <a href="./index.html">Sair</a>
                        </div>
                    </div>
                </nav>
            </header>
            <main className="mainContainer">
                <article className="articleHeader">
                    <h1 className="title">Meus Projetos</h1>
                    <a id="openForm" className="newProject" href="#">Novo Projeto</a>
                    <div id="menuSidebar" className="sidebar">
                        <form className="formContainer">
                            <input type="text" placeholder="Nome do Projeto" />
                            <input type="text" placeholder="Descrição do Projeto" />
                            <input type="text" placeholder="URL do Git" />
                            <input type="text" placeholder="Branch do Git" />
                            <input
                                type="text"
                                placeholder="Usuário do Repositório"
                            />
                            <input type="text" placeholder="Email do Repositório" />
                            <input type="email" placeholder="URL do Sast" />
                            <input type="email" placeholder="Usuário do Sast" />
                            <input type="email" placeholder="Senha do Sast" />
                            <input type="email" placeholder="URL do Dast" />
                            <input type="email" placeholder="Usuário do Dast" />
                            <input type="email" placeholder="Senha do Dast" />
                            <div className="dropdown">
                                <button ref={dropbtnRef} className="dropbtn" onClick={handleClick}>
                                    Selecione uma Linguagem
                                </button>
                                <div ref={dropdownContentRef} className={`dropdownContent ${show ? 'show' : ''}`} id="dropdownContent">
                                    <a href="#">JavaScript</a>
                                    <a href="#">Python</a>
                                    <a href="#">Java</a>
                                    <a href="#">C++</a>
                                    <a href="#">C#</a>
                                    <a href="#">PHP</a>
                                    <a href="#">Ruby</a>
                                    <a href="#">Swift</a>
                                    <a href="#">Kotlin</a>
                                    <a href="#">Go (Golang)</a>
                                    <a href="#">R</a>
                                    <a href="#">TypeScript</a>
                                    <a href="#">Scala</a>
                                    <a href="#">Perl</a>
                                    <a href="#">Lua</a>
                                    <a href="#">Assembly</a>
                                    <a href="#">Dart</a>
                                    <a href="#">Rust</a>
                                    <a href="#">Swift</a>
                                </div>
                                <div className="buttons">
                                    <button className="submit" type="submit">
                                        Enviar Projeto
                                    </button>
                                    <button className="close" id="closeForm">
                                        Fechar
                                    </button>
                                </div>
                            </div>
                        </form>
                    </div>
                </article>
                <article className="articleContainer">
                    <article className="articleItem">
                        <h3>Projeto</h3>
                        <div className="articleLink">
                            <a href="./"
                            ><img src="./assets/img/edit.svg" alt=""
                                /></a>
                            <a href="./"
                            ><img src="./assets/img/trash.svg" alt=""
                                /></a>
                            <a href="./"
                            ><img src="./assets/img/pull.svg" alt=""
                                /></a>
                        </div>
                    </article>
                    <article className="articleItem">
                        <h3>Projeto</h3>
                        <div className="articleLink">
                            <a href="./"
                            ><img src="./assets/img/edit.svg" alt=""
                                /></a>
                            <a href="./"
                            ><img src="./assets/img/trash.svg" alt=""
                                /></a>
                            <a href="./"
                            ><img src="./assets/img/pull.svg" alt=""
                                /></a>
                        </div>
                    </article>
                    <article className="articleItem">
                        <h3>Projeto</h3>
                        <div className="articleLink">
                            <a href="./"
                            ><img src="./assets/img/edit.svg" alt=""
                                /></a>
                            <a href="./"
                            ><img src="./assets/img/trash.svg" alt=""
                                /></a>
                            <a href="./"
                            ><img src="./assets/img/pull.svg" alt=""
                                /></a>
                        </div>
                    </article>
                    <article className="articleItem">
                        <h3>Projeto</h3>
                        <div className="articleLink">
                            <a href="./"
                            ><img src="./assets/img/edit.svg" alt=""
                                /></a>
                            <a href="./"
                            ><img src="./assets/img/trash.svg" alt=""
                                /></a>
                            <a href="./"
                            ><img src="./assets/img/pull.svg" alt=""
                                /></a>
                        </div>
                    </article>
                    <article className="articleItem">
                        <h3>Projeto</h3>
                        <div className="articleLink">
                            <a href="./"
                            ><img src="./assets/img/edit.svg" alt=""
                                /></a>
                            <a href="./"
                            ><img src="./assets/img/trash.svg" alt=""
                                /></a>
                            <a href="./"
                            ><img src="./assets/img/pull.svg" alt=""
                                /></a>
                        </div>
                    </article>
                    <article className="articleItem">
                        <h3>Projeto</h3>
                        <div className="articleLink">
                            <a href="./"
                            ><img src="./assets/img/edit.svg" alt=""
                                /></a>
                            <a href="./"
                            ><img src="./assets/img/trash.svg" alt=""
                                /></a>
                            <a href="./"
                            ><img src="./assets/img/pull.svg" alt=""
                                /></a>
                        </div>
                    </article>
                    <article className="articleItem">
                        <h3>Projeto</h3>
                        <div className="articleLink">
                            <a href="./"
                            ><img src="./assets/img/edit.svg" alt=""
                                /></a>
                            <a href="./"
                            ><img src="./assets/img/trash.svg" alt=""
                                /></a>
                            <a href="./"
                            ><img src="./assets/img/pull.svg" alt=""
                                /></a>
                        </div>
                    </article>
                    <article className="articleItem">
                        <h3>Projeto</h3>
                        <div className="articleLink">
                            <a href="./"
                            ><img src="./assets/img/edit.svg" alt=""
                                /></a>
                            <a href="./"
                            ><img src="./assets/img/trash.svg" alt=""
                                /></a>
                            <a href="./"
                            ><img src="./assets/img/pull.svg" alt=""
                                /></a>
                        </div>
                    </article>
                    <article className="articleItem">
                        <h3>Projeto</h3>
                        <div className="articleLink">
                            <a href="./"
                            ><img src="./assets/img/edit.svg" alt=""
                                /></a>
                            <a href="./"
                            ><img src="./assets/img/trash.svg" alt=""
                                /></a>
                            <a href="./"
                            ><img src="./assets/img/pull.svg" alt=""
                                /></a>
                        </div>
                    </article>
                    <article className="articleItem">
                        <h3>Projeto</h3>
                        <div className="articleLink">
                            <a href="./"
                            ><img src="./assets/img/edit.svg" alt=""
                                /></a>
                            <a href="./"
                            ><img src="./assets/img/trash.svg" alt=""
                                /></a>
                            <a href="./"
                            ><img src="./assets/img/pull.svg" alt=""
                                /></a>
                        </div>
                    </article>
                    <article className="articleItem">
                        <h3>Projeto</h3>
                        <div className="articleLink">
                            <a href="./"
                            ><img src="./assets/img/edit.svg" alt=""
                                /></a>
                            <a href="./"
                            ><img src="./assets/img/trash.svg" alt=""
                                /></a>
                            <a href="./"
                            ><img src="./assets/img/pull.svg" alt=""
                                /></a>
                        </div>
                    </article>
                </article>
            </main>
            <section className="menuDock">
                <div className="mainMenuContainer">
                    <a className="logoMenu" href="#"></a>
                    <a className="logoMenu" href="#"></a>
                    <a className="logoMenu" href="#"></a>
                    <a className="logoMenu" href="#"></a>
                </div>
                <button className="buttonMenu" id="showMenuBtn">
                    <img className="arrowUp" src="./assets/img/arrowDown.svg" alt="sdf" />
                </button>
                <button className="buttonMenu" id="hideMenuBtn" hidden>
                    <img
                        className="arrowDown"
                        src="./assets/img/arrowDown.svg"
                        alt=""
                    />
                </button>
            </section>
            <span id="btnBackToTop" className="hidden">
                <img src="./assets/img/arrow.svg" alt="#"/>
            </span>
            <footer className="footerContainer">
                <ul className="footerLinks">
                    <a href="#"
                    ><img className="logo" src="./assets/img/logo.svg" alt=""
                        /></a>
                    <div className="footerButtonLinks">
                        <li><a href="#">Lorem Ipsum</a></li>
                        <li><a href="#">Lorem Ipsum</a></li>
                        <li><a href="#">Lorem Ipsum</a></li>
                        <li><a href="#">Lorem Ipsum</a></li>
                    </div>
                    <p className="copyright">© Copyright 2024 the Eduardzs</p>
                </ul>
            </footer>
        </div>
    )
}

export default Projetos;
