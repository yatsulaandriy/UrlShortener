import React, { useState, useEffect } from 'react';

const App = () =>
{
  const [urls, setUrls] = useState([]);
  const [loading, setLoading] = useState(true);
  const [showModal, setShowModal] = useState(false);
  const [longUrl, setLongUrl] = useState('');

  const handleInputChange = (event) => 
  {
    setLongUrl(event.target.value);
  }

  const handleAddUrlClick = () => 
  {
    setShowModal(true);
  };

  const handleSubmit = async () =>
  {
    const encodedUrl = encodeURIComponent(longUrl);
    const response = await fetch('https://localhost:7020/UrlMap/AddUrlMap/' + encodedUrl,
    {
      method: 'POST',
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });

    const data = await response.json();

    setUrls(urls => urls.concat(data));
    console.log(data);

    setLongUrl('');
  };

  const handleDeleteUrlClick = async (urlMapId) =>
  {
    await fetch('https://localhost:7020/UrlMap/RemoveUrlMap/' + urlMapId, {method: 'DELETE'});
    const updatedUrls = urls.filter(url => url.id !== urlMapId);
    setUrls(updatedUrls);
  }

  const handleAddUrlClose = () => 
  {
    setShowModal(false);
  };

  useEffect(() => 
  {
    fetchUrls();
  }, []);

  const fetchUrls = async () => 
  {
    const response = await fetch('https://localhost:7020/UrlMap/GetUrlMaps');
    const data = await response.json();
    setUrls(data);
    setLoading(false);
  }

  const renderUrlsTable = (fetchUrls) => 
  {
    return (
      <>
        <table>
          <thead>
            <tr>
              <th>Long</th>
              <th>Short</th>
            </tr>
          </thead>
          <tbody>
            {fetchUrls.map(url =>
              <tr key={url.id}>
                <td><a href={url.longUrl}>{url.longUrl}</a></td>
                <td><a href={'https://localhost:7020/' + url.shortUrl}>{'https://localhost:7020/' + url.shortUrl}</a></td>
                <td>{<button onClick={() =>  handleDeleteUrlClick(url.id) }>Delete</button>}</td>
              </tr>
            )}
          </tbody>
        </table>

        <button onClick={handleAddUrlClick}>Add Shortened Url</button>
      </>
    );
  }

  const renderShortUrlForm = () => {
    return (
      <div>
        {showModal && (
          <div className="modal">
            <div className="modal-content">
              <span className="close" onClick={handleAddUrlClose}>
                &times;
              </span>
              <h2>Add new shortened url</h2>
              <input type="text" value={longUrl} onChange={handleInputChange} />
              <button onClick={handleSubmit}>Submit</button>
            </div>
          </div>
        )}
      </div>
    );
  };

  let content = loading
  ? <p>Loading</p>
  : renderUrlsTable(urls);

  return (
    <div>
      {content}
      {renderShortUrlForm()}
    </div>
  );
}

export default App;
