import { faSave } from "@fortawesome/free-solid-svg-icons";
import { ApolloClient, InMemoryCache, gql } from "@apollo/client";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { invoke } from "@tauri-apps/api/core";
import { useState } from "react";
import { Col, Form, FormGroup, Row } from "react-bootstrap";
import styled from "styled-components";



const CustomButtonStyle = styled.button`
        height: 2.5rem;
        margin-top: 1rem;
        text-align: center;
        justify-content: center;
        align-items: center;
        display: flex;
        background-color: var(--background-color-primary);
        --bs-btn-active-bg: var(--background-color-terciario);
        border: 3px solid var(--background-color-secondary);
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

const langs = ["C",
    "C_PLUS_PLUS",
    "C_SHARP",
    "CSS",
    "GO",
    "HTML",
    "JAVA",
    "JAVA_SCRIPT",
    "KOTLIN",
    "OBJECTIVE_C",
    "OTHER",
    "PERL",
    "PHP",
    "PYTHON",
    "RUBY",
    "RUST",
    "SCALA",
    "SHELL",
    "SWIFT",
    "TYPE_SCRIPT"]


export function CreateProject({ onSubmit }: Readonly<{ onSubmit: () => void }>) {
    const [form, setForm] = useState(
        {
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
            dastUIurl: "",
            dastApis: "",
            dastGraphql: ""
        });
    const handleChange = (event: { target: { name: any; value: any; }; }) => {
        setForm({
            ...form,
            [event.target.name]: event.target.value
        });
    };
    function handleSubmit(e: any) {
        e.preventDefault();
        e.stopPropagation();
        let sub = {
            ...form,
            dastUIurl: form.dastUIurl.split(";"),
            dastApis: form.dastApis.split(";"),
            dastGraphql: form.dastGraphql.split(";")
        }
        invoke("url_server").then((response) => {
            const _url = new URL(response as string);

            const client = new ApolloClient({
                uri: (new URL("/graphql", _url.toString())).toString(),
                cache: new InMemoryCache(),
                headers: {
                    "Authorization": "Bearer " + localStorage.getItem("token"),
                }
              });


              client.mutate({
                mutation: gql`
                mutation CreateProject($request: CreateProjectRequestInput!) {
                    createProject(request: $request)
                  }
               `,
               variables: {
                request: {
                  branchGit: sub.branchGit,
                  description: sub.description,
                  emailRepository: sub.emailRepository,
                  language: sub.language,
                  name: sub.name,
                  tokenRepository: sub.tokenRepository,
                  urlGit: sub.urlGit,
                  userRepository: sub.userRepository,
                  dockerfileMultiStage: sub.dockerfileMultiStage,
                  dockerContextPath: sub.dockerContextPath,
                  version: sub.version,
                  dastUIurl: sub.dastUIurl,
                  dastApis: null,
                  dastGraphql: null
                }
              }
              }).then((response) => {
                console.log(response);
              });
        });
        onSubmit();
    }
    return (
        <Form onSubmit={handleSubmit}>
            <Row className="mb-1">
                <FormGroup title="project" as={Col}>
                    <Form.Label>Name</Form.Label>
                    <Form.Control defaultValue={""} name="name" value={form.name} onChange={handleChange} className="input" type="text" placeholder="project name" />
                </FormGroup>
                <FormGroup title="project" as={Col}>
                    <Form.Label>Version</Form.Label>
                    <Form.Control name="version" value={form.version} onChange={handleChange} className="input" type="text" placeholder="project version" />
                </FormGroup>
                <FormGroup title="project" as={Col}>
                    <Form.Label htmlFor="language">Lang</Form.Label>
                    <Form.Select  id="language" name="language" value={form.language} onChange={handleChange} className="input">
                        {langs.map((lang) => <option key={lang} value={lang}>{lang}</option>)}
                    </Form.Select>
                </FormGroup>
            </Row>
            <Row className="mb-1">
                <FormGroup title="project" as={Col}>
                    <Form.Label>Description</Form.Label>
                    <Form.Control name="description" as="textarea" value={form.description} onChange={handleChange} rows={3} className="input" type="text" placeholder="Enter project description" />
                </FormGroup>
            </Row>
            <Row className="mb-1">
                <FormGroup title="repository" as={Col}>
                    <Form.Label>Repository</Form.Label>
                    <Form.Control name="emailRepository" value={form.emailRepository} onChange={handleChange} className="input mb-1" type="password" placeholder="Enter repository e-mail" />
                    <Form.Control name="userRepository" value={form.userRepository} onChange={handleChange} className="input mb-1" type="password" placeholder="Enter repository user" />
                    <Form.Control name="tokenRepository" value={form.tokenRepository} onChange={handleChange} className="input mb-1" type="password" placeholder="Enter repository token" />
                    <Form.Control name="branchGit" value={form.branchGit} onChange={handleChange} className="input mb-1" type="password" placeholder="Enter repository branch git" />
                    <Form.Control name="urlGit" value={form.urlGit} onChange={handleChange} className="input mb-1" type="password" placeholder="Enter repository url git" />
                </FormGroup>
            </Row>
            <Row className="mb-1">
                <FormGroup title="Dast" as={Col}>
                    <Form.Label>Dast</Form.Label>
                    <Form.Control name="dastUIurl" value={form.dastUIurl} onChange={handleChange} className="input" placeholder="Enter server urls, sep ';'" />
                </FormGroup>
            </Row>
            <Row className="mb-1">
                <FormGroup title="docker" as={Col}>
                    <Form.Label>Docker</Form.Label>
                    <Form.Control name="dockerContextPath" value={form.dockerContextPath} onChange={handleChange} className="input mb-1" type="text" placeholder="Enter dockerfile context dir" />
                    <Form.Control name="dockerfileMultiStage" value={form.dockerfileMultiStage} onChange={handleChange} as="textarea" rows={3} className="input mb-1" type="text" placeholder="Enter dockerfile" />
                </FormGroup>
            </Row>
            <div style={{ width: "100%", display: "flex", justifyContent: "flex-end", textAlign: "end", alignItems: "end" }}>
                <CustomButtonStyle className='btn'>
                    <FontAwesomeIcon icon={faSave} />
                </CustomButtonStyle>
            </div>
        </Form>)
}
