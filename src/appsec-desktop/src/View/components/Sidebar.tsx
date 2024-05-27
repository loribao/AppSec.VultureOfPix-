import {  styled } from 'styled-components';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faPlus, faPlay, faTrash, faEdit, faSave } from '@fortawesome/free-solid-svg-icons'
import { useEffect, useState } from 'react';
import { invoke } from '@tauri-apps/api/core';
import { Modal } from 'react-bootstrap';
import { CreateProject } from './CreateProject';
const CustomButtonStyle = styled.button`
        height: 2.5rem;
        margin-top: 1rem;
        text-align: center;
        justify-content: center;
        align-items: center;
        display: flex;
        background-color: var(--background-color-primary);
        --bs-btn-active-bg: var(--background-color-terciario);
        border: 3px solid var(--contrast-color);
        color: var(--color-title-one);
    &:hover {
        border: 3px solid var(--contrast-color);
        border-radius: 6px;
        background-color: var(--background-color-secondary);
    }
  & > svg {
       width: 100%;
       height: 100%;
       color: var(--color-title-one);
    }
`
const ProjectActionsStyle = styled.div`
    width: 100%;
    height: 100%;
    display: flex;
    gap: 1rem;
    justify-content: end;
    align-items: center;
    & > svg {
       max-width: 50px;
       max-height: 50px;
       color: var(--color-title-one);
    }
    & > svg:hover {
        border: 3px solid var(--contrast-color);
        border-radius: 6px;
        background-color: var(--color-title-one);
    }
`
const ProjectStyle = styled.div`
        max-height: 5.5rem;
        margin-top: 1rem;
        text-align: center;
        justify-content: start;
        align-items: stretch;
        display: flex;
        flex-direction: row;
        gap: 0.4rem;
        flex-wrap: row wrap;
        background-color: var(--background-color-primary);
        --bs-btn-active-bg: var(--background-color-terciario);
        border: 3px solid var(--contrast-color);
        color: var(--color-title-one);
    &:hover {
        border: 3px solid var(--contrast-color);
        border-radius: 6px;
        background-color: var(--background-color-secondary);
    }

`

const SidebarStyle = styled.div`
    box-shadow: 0 .5rem 0.6rem #544692;
    font-family: "Inter", sans-serif;
    background-color:#322a57;
    color: var(--color-title-one);
`

export default function Sidebar() {

    const [showCreateProject, setShowCreateProject] = useState(false);
    const handleCloseCreateProject = () => setShowCreateProject(false);
    const handleShowCreateProject = () => setShowCreateProject(true);
    const [projects, setProjects] = useState([{
            id: "",
            branchGit: "",
            description: "",
            emailRepository: "",
            language: "C_SHARP",
            name: "",
            tokenRepository: "",
            urlGit: "",
            userRepository: "",
            dockerfileMultiStage: "",
            dockerContextPath: ".",
            version: "",
            dastUIurl: [""],
            dastApis: [""],
            dastGraphql: [""]
    }]);
    const [url, setUrl] = useState(new URL("https://localhost:5081"));

    useEffect(() => {
        setInterval(() => {
            console.log("fetching projects");
            fetch(new URL("/graphql", url), {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + localStorage.getItem('token')
                },
                body: JSON.stringify({
                    query: `query {projects{name,id}}`
                })
            }).then(response => response.json()
            ).then(data => {
                setProjects(data.data.projects);
            })
        }, 5000);
    }, []);

    function StartProject(projectName: string) {
        invoke("url_server").then((response) => {
            const _url = new URL(response as string);

            fetch(new URL("/graphql", _url), {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + localStorage.getItem('token')
                },
                body: JSON.stringify({
                    query: `
                    mutation Run($appname: String!) {
                      startDiffRepository(request: {projectName: $appname})
                      startDast(request: {projectName:  $appname})
                      startSast(request: {nameProject:  $appname})
                    }`,
                    variables: { "appname": projectName }
                })
            }).then(response => response.json()
            ).then(data => {
                console.log(data);
            })
        });

    }
    return (
        <SidebarStyle>
            <div className='d-flex justify-content-center'>
                <CustomButtonStyle className='btn'>
                    <FontAwesomeIcon icon={faPlus} onClick={handleShowCreateProject} />
                </CustomButtonStyle>
            </div>
            <div>
                {projects.map((project, index) => {
                    return (
                        <div key={project.id} className='d-flex justify-content-center'>
                            <ProjectStyle style={{ width: "98%" }} className='btn'>
                                <label htmlFor={project.id}>{project.name} </label>
                                <ProjectActionsStyle>
                                    <FontAwesomeIcon icon={faPlay} onClick={()=>StartProject(project.name)}/>
                                    <FontAwesomeIcon icon={faTrash} />
                                </ProjectActionsStyle>
                            </ProjectStyle>
                        </div>
                    )
                })}
            </div>
            <CreateProjectModal show={showCreateProject} handleClose={handleCloseCreateProject} handleShow={handleShowCreateProject} />
        </SidebarStyle>
    )
}
function CreateProjectModal({ show, handleClose, handleShow }: { show: boolean, handleClose: () => void, handleShow: () => void}) {
    return (
        <Modal size='lg' contentClassName='text-bg-dark' show={show} onHide={handleClose} onShow={handleShow} onCl>
            <Modal.Header closeButton>
                <Modal.Title>Modal heading</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <CreateProject onSubmit={handleClose} />
            </Modal.Body>
        </Modal>
    );
}
