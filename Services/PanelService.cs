using Evaluation.Portal.API.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Evaluation.Portal.API.Services
{
    public class PanelService
    {
        private readonly IMongoCollection<Panel> _panels;
        public PanelService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _panels = database.GetCollection<Panel>(settings.PanelsCollectionName);

        }

        public List<Panel> Get()
        {
            List<Panel> users;
            users = _panels.Find(x => true).ToList();
            return users;
        }

        public Panel Get(string id) =>
            _panels.Find<Panel>(p => p.PanelId == id).FirstOrDefault();

        public void InsertPanel(Panel panel)
        {
            _panels.InsertOneAsync(panel).ConfigureAwait(false);
        }

        public void UpdatePanel(Panel panel)
        {
            UpdateDefinition<Panel> updateDefinition = Builders<Panel>.Update
                 .Set(m => m.CandidateId, panel.CandidateId)
                 .Set(m => m.CandidateName, panel.CandidateName)
                 .Set(m => m.PanelDate, panel.PanelDate)
                 .Set(m => m.PanelTimeZone, panel.PanelTimeZone)
                 .Set(m => m.TimeAndDate, panel.TimeAndDate)
                 .Set(m => m.TrAssociate, panel.TrAssociate)
                 .Set(m => m.MrAssociate, panel.MrAssociate)
                 .Set(m => m.SelectedTr, panel.SelectedTr)
                 .Set(m => m.SelectedMr, panel.SelectedMr)
                 .Set(m => m.UpdatedDate, panel.UpdatedDate)
                 .Set(m => m.UpdatedBy, panel.UpdatedBy);

            _panels.UpdateOneAsync(Builders<Panel>.Filter.Eq(c => c.PanelId, panel.PanelId), updateDefinition, new UpdateOptions { IsUpsert = true });
        }

        public void DeletePanel(string panelId)
        {
            FilterDefinition<Panel> filterDefinition = Builders<Panel>.Filter.Eq(u => u.PanelId, panelId);
            _panels.DeleteOneAsync(filterDefinition).ConfigureAwait(false);
        }
    }
}
