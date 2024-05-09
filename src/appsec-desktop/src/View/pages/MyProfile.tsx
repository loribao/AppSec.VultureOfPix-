import "./MyProfile.css";
import { Header } from "../components/Header";
import "../components/Footer";
import "../components/PageClimp";
import banner from "../assets/img/profileBanner.png";
import profile from "../assets/img/avatar.png";
import imgDisconnectButton from "../assets/img/personCheck.svg";
import imgEditProfile from "../assets/img/edit.svg";
import Footer from "../components/Footer";
import PageClimp from "../components/PageClimp";
import MenuDock from "../components/MenuDock";

function MyProfile() {
    return (
        <div>
            <Header />
            <main className="mainMyProfileContainer">
                <div className="profileContainer">
                    <img className="banner" src={banner} alt="" />
                    <div className="profileInfo">
                        <div>
                            <img className="profile" src={profile} alt="" />
                        </div>
                        <div className="infoContainer">
                            <h2>Eduardo Fonseca</h2>
                            <p>Desenvolvedor Front-end Web</p>
                            <p>Minas Gerais, Brasil</p>
                            <p>20 Anos</p>
                        </div>
                        <div className="buttonContainer">
                            <div>
                                <a className="buttonEditProfile" href="">
                                    <img
                                        className="imgDisconnectButton"
                                        src={imgEditProfile}
                                        alt=""
                                    />
                                    Editar Perfil
                                </a>
                            </div>
                            <div>
                                <a className="disconnectButton" href="">
                                    <img
                                        className="imgDisconnectButton"
                                        src={imgDisconnectButton}
                                        alt=""
                                    />
                                    Conectado
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="dashboardsContainer">
                    <div className="dashboards">
                        <div className="primaryContainer">
                            <h4>SOBRE</h4>
                            <p>
                                <strong>Nome Completo: </strong>Eduardo Fonseca
                            </p>
                            <p>
                                <strong>Status: </strong>Ativo
                            </p>
                            <p>
                                <strong>Papel: </strong>Desenvolvedor Front-end
                                Web
                            </p>
                            <p>
                                <strong>País: </strong>Brasil
                            </p>
                            <p>
                                <strong>Linguagem: </strong>Português
                            </p>
                            <h4>CONTATOS</h4>
                            <p>
                                <strong>Telefone: </strong>+55 (38) 99895-0689
                            </p>
                            <p>
                                <strong>E-mail: </strong>
                                eduardofonseca0210@gmail.com
                            </p>
                            <h4>TIMES</h4>
                            <p>
                                <strong>Frontend Developer </strong> (126
                                Membros)
                            </p>
                            <p>
                                <strong>React Developer</strong> (96 Membros)
                            </p>
                        </div>
                        <div className="primaryContainer">
                            <h3>Cronograma de Atividades</h3>
                        </div>
                        <div className="primaryContainer overview">
                            <h4>VISÃO GERAL</h4>
                            <p>
                                <strong>Tarefa Compilada:</strong> 13,5 mil
                            </p>
                            <p>
                                <strong>Conexões:</strong> 897
                            </p>
                            <p>
                                <strong>Projetos compilados:</strong> 146
                            </p>
                        </div>
                        <div className="connectionsTeamsContainer">
                            <div className="primaryContainer connectionsTeams">
                                <h3>Conexões</h3>
                            </div>
                            <div className="primaryContainer connectionsTeams">
                                <h3>Equipes</h3>
                            </div>
                        </div>
                        <div className="primaryContainer projects">
                            <h3>Projetos</h3>
                        </div>
                    </div>
                </div>
            </main>
            <Footer />
            <PageClimp />
            <MenuDock />
        </div>
    );
}

export default MyProfile;
