import './Dashboard.css';
import SideNavBar from '../components/Sidebar';
import { CardBody, CardHeader } from 'react-bootstrap';
import styled from 'styled-components';
import { OverallCode } from '../components/OverallCode';


const ContainerStyle = styled.div`
    padding: 0;
    margin: 0;
    display: flex;
    flex-direction: column;
    flex-wrap: wrap;
    justify-content: stretch;
    align-items: stretch;
    align-content: stretch;
`
const DashboardContainerSty = styled.div`
    width: 100vw;
    height: 100vh;
    padding: 0;
    margin: 0;
    display: grid;
    grid-template-columns: 20vw auto;
`
const CardStyleVulture = styled.div`
    --bs-card-spacer-y: 1rem;
    --bs-card-spacer-x: 1rem;
    --bs-card-title-spacer-y: 0.5rem;
    --bs-card-title-color: ;
    --bs-card-subtitle-color: ;
    --bs-card-border-width: var(--bs-border-width);
    --bs-card-border-color: var(--bs-border-color-translucent);
    --bs-card-border-radius: var(--bs-border-radius);
    --bs-card-box-shadow: ;
    --bs-card-inner-border-radius: calc(var(--bs-border-radius) - (var(--bs-border-width)));
    --bs-card-cap-padding-y: 0.5rem;
    --bs-card-cap-padding-x: 1rem;
    --bs-card-cap-bg: rgba(var(--bs-body-color-rgb), 0.03);
    --bs-card-cap-color: ;
    --bs-card-height: ;
    --bs-card-color: ;
    --bs-card-bg: var(--bs-body-bg);
    --bs-card-img-overlay-padding: 1rem;
    --bs-card-group-margin: 0.75rem;
    position: relative;
    display: flex;
    box-shadow: 0 .5rem 1rem #544692;
    flex-direction: column;
    min-width: 0;
    color: var(--color-title-one);
    word-wrap: break-word;
    background-color: #322a57;
    background-clip: border-box;
    border: var(--bs-card-border-width) solid var(--bs-card-border-color);
    border-radius: var(--bs-card-border-radius);
    margin: 0;
    padding: 0;
    align-content: center;
    width: 100%;
`
export default function Dashboard() {
    return (
        <DashboardContainerSty className='container container-fluid'>
            <SideNavBar />
            <iframe src='https://localhost:5081/graphql' ></iframe>
            <ContainerStyle>
                <CardStyleVulture  >
                    <CardHeader> <h3>Sast Alert</h3> </CardHeader>
                    <CardBody>
                        <OverallCode />
                    </CardBody>
                </CardStyleVulture>
                <CardStyleVulture>
                    <CardHeader> <p>Sast - Mensures</p> </CardHeader>
                    <CardBody>
                        <div className="chart-container">

                        </div>
                    </CardBody>
                </CardStyleVulture>
                <CardStyleVulture>
                    <CardHeader> <p>Sast - Mensures</p> </CardHeader>
                    <CardBody>
                        <div className="chart-container">
                            <svg id="chart"></svg>
                        </div>
                    </CardBody>
                </CardStyleVulture>
            </ContainerStyle>
        </DashboardContainerSty>
    )
}
