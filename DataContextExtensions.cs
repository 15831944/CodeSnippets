public static class DataContextExtensions
    {
        /// <summary>
        ///     Discard all pending changes of current DataContext.
        ///     All un-submitted changes, including insert/delete/modify will lost.
        /// </summary>
        /// <param name="context"></param>
        public static void DiscardPendingChanges(this DataContext context)
        {
            context.RefreshPendingChanges(RefreshMode.OverwriteCurrentValues);
            ChangeSet changeSet = context.GetChangeSet();
            if (changeSet != null)
            {
                //Undo inserts
                foreach (object objToInsert in changeSet.Inserts)
                {
                    context.GetTable(objToInsert.GetType()).DeleteOnSubmit(objToInsert);
                }
                //Undo deletes
                foreach (object objToDelete in changeSet.Deletes)
                {
                    context.GetTable(objToDelete.GetType()).InsertOnSubmit(objToDelete);
                }
            }
        }
 
        /// <summary>
        ///     Refreshes all pending Delete/Update entity objects of current DataContext according to the specified mode.
        ///     Nothing will do on Pending Insert entity objects.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="refreshMode">A value that specifies how optimistic concurrency conflicts are handled.</param>
        public static void RefreshPendingChanges(this DataContext context, RefreshMode refreshMode)
        {
            ChangeSet changeSet = context.GetChangeSet();
            if (changeSet != null)
            {
                context.Refresh(refreshMode, changeSet.Deletes);
                context.Refresh(refreshMode, changeSet.Updates);
            }
        }
    }
