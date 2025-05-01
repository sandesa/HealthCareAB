import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import '../../index.css'
import '../accountInfo.css'
import AddAvailForm from './addAvailForm.tsx'
import Navbar from '../../navbar/navbar.tsx'
import AccountSidebar from '../accountSidebar.tsx'

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

createRoot(document.getElementById('avail-form')!).render(
    <StrictMode>
        <AddAvailForm />
    </StrictMode>,
)