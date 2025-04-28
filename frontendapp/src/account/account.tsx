import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import '../index.css'
import './account.css';
import AccountInformation from './displayAccountDetails.tsx'
import Navbar from '../navbar/navbar.tsx'
import AccountSidebar from './accountSidebar.tsx'
import PreviousAppointment from './previousAppointment.tsx'

createRoot(document.getElementById('navbar')!).render(
    <StrictMode>
        <Navbar />
    </StrictMode>,
)

createRoot(document.getElementById('account-sidebar')!).render(
    <StrictMode>
        <AccountSidebar />
    </StrictMode>,
)

createRoot(document.getElementById('account-information')!).render(
    <StrictMode>
        <AccountInformation />
    </StrictMode>,
)

createRoot(document.getElementById('previous-appointment')!).render(
    <StrictMode>
        <PreviousAppointment />
    </StrictMode>,
)