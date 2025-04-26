import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import AccountInformation from './displayAccountDetails.tsx'
import Navbar from './navbar.tsx'


createRoot(document.getElementById('root')!).render(
    <StrictMode>
        <AccountInformation />
    </StrictMode>,
)

createRoot(document.getElementById('navbar')!).render(
    <StrictMode>
        <Navbar />
    </StrictMode>,
)