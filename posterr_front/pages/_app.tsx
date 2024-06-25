import '../styles/globals.css';
import { AppProps } from 'next/app';
import { UserProvider } from '../context/userContext'
import Layout from '../components/layout';
import { Toaster } from 'react-hot-toast';

function MyApp({ Component, pageProps }: AppProps) {
  return (
    <UserProvider>
      <Layout>
        <Component {...pageProps} />
        <Toaster position='top-right'/>
      </Layout>
    </UserProvider>
  );
}

export default MyApp;
